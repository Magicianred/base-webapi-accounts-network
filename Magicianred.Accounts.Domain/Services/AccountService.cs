using Magicianred.Accounts.Domain.Enums;
using Magicianred.Accounts.Domain.Interfaces;
using Magicianred.Accounts.Domain.Interfaces.Handlers;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Repositories;
using Magicianred.Accounts.Domain.Interfaces.Services.Communication;
using Magicianred.Accounts.Domain.Services.Communication;
using System.Linq;
using System.Threading.Tasks;

namespace Magicianred.Accounts.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHandler _passwordHandler;

        public AccountService(IAccountRepository accountRepository, IRoleRepository roleRepository, IAccountRoleRepository accountRoleRepository, IUnitOfWork unitOfWork, IPasswordHandler passwordHandler)
        {
            _passwordHandler = passwordHandler;
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _accountRoleRepository = accountRoleRepository;
        }

        public async Task<ICreateUserResponse> CreateAccountAsync(IAccount account, params IRole[] accountRoles)
        {
            var existingUser = await _accountRepository.FindByEmailAsync(account.Email);
            if (existingUser != null)
            {
                return new CreateUserResponse(false, "Email already in use.", null);
            }

            account.Password = _passwordHandler.HashPassword(account.Password);

            await _accountRepository.CreateAsync(account, EntityTypesEnum.USER);
            await _accountRoleRepository.AddAsync(account, accountRoles);
            await _unitOfWork.CompleteAsync();

            return new CreateUserResponse(true, null, account);
        }

        public async Task<IAccount> FindByEmailAsync(string email)
        {
            var account = await _accountRepository.FindByEmailAsync(email);
            if (account != null && account.AccountRoles == null)
            {
                var accountRoles = await _accountRoleRepository.FindByAccountIdAsync(account.Id);
                account.AccountRoles = accountRoles.ToList();
                if (account.AccountRoles != null)
                {
                    if (account.AccountRoles.Count > 0 && account.AccountRoles[0].Role == null)
                        foreach (var accountRole in account.AccountRoles)
                        {
                            accountRole.Account = account;
                            accountRole.Role = await _roleRepository.FindByIdAsync(accountRole.RoleId);
                        }
                }
            }

            return account;
        }
    }
}