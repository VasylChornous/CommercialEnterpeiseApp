using CommercialEnterprise.Core.Exceptions;
using CommercialEnterprise.Core.Services.Interfaces;
using CommercialEnterprise.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommercialEnterprise.UI
{
    public class AuthorizationPage
    {
        private readonly ILoginService loginService;
        private User currentUser = null;
        public AuthorizationPage(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        public async Task<User> ShowAuth()
        {
            while (true)
            {
                Console.Clear();

                ConsoleSettingsChanger.DisplayGreenMessage("Would you like to?: ");
                ConsoleSettingsChanger.DisplayRedMessage("<0> - to close app;");
                ConsoleSettingsChanger.DisplayGreenMessage("<1> - to register new account;");
                ConsoleSettingsChanger.DisplayGreenMessage("<2> - to authorize;");
                ConsoleSettingsChanger.DisplayRedMessage("<9> - to restore password by key word;");
                Console.Write("Your choice: ");

                var authChoose = Console.ReadLine();

                switch (authChoose)
                {
                    case "0":
                        Environment.Exit(0);
                        break;
                    case "1":
                        await Register();
                        break;
                    case "2":
                        await LogIn();
                        break;
                    case "9":
                        await RestorePassword();
                        break;
                    default:
                        break;
                }

                if (currentUser != null)
                {
                    return currentUser;
                }
            }
        }
        public async Task RestorePassword()
        {
            Console.Clear();
            ConsoleSettingsChanger.DisplayRedMessage("<---- RESTORING PASSWORD ---->");
            Console.WriteLine();

            try
            {
                Console.Write("[RESTORE] Your login: ");
                var login = Console.ReadLine();

                Console.Write("[RESTORE] Your key word: ");
                var keyWord = Console.ReadLine();

                if (!await loginService.IsUserExistsWithLoginAndKeyWord(login, keyWord))
                {
                    throw new UserException("\n[ERROR] User is not exist!");
                }

                Console.Write("\n[RESTORE] New password: ");
                var newPassword = Console.ReadLine();

                await loginService.RestorePasswordAsync(login, keyWord, newPassword);

                ConsoleSettingsChanger.DisplayGreenMessage("\n[SUCCESS] User password has been restored :)");
            }
            catch (UserException e)
            {
                ConsoleSettingsChanger.DisplayRedMessage(e.Message);
            }

            Thread.Sleep(5000);
            await ShowAuth();
        }
        public async Task Register()
        {
            while (true)
            {
                try
                {
                    Console.Clear();

                    ConsoleSettingsChanger.DisplayRedMessage("<-----USER-REGISTRATION----->");
                    Console.WriteLine();

                    Console.Write("[REGISTRATION] LOGIN: ");
                    var login = Console.ReadLine();

                    Console.Write("[REGISTRATION] PASSWORD: ");
                    var password = Console.ReadLine();

                    Console.Write("[REGISTRATION] CONFIRM PASSWORD: ");
                    var confirmedPassword = Console.ReadLine();
                    if (password != confirmedPassword)
                        throw new RegistrationException("[ERROR] Password was not confirmed!");

                    Console.Write("[REGISTRATION] NAME: ");
                    var name = Console.ReadLine();

                    Console.Write("[REGISTRATION] SURNAME: ");
                    var surname = Console.ReadLine();

                    Console.Write("[REGISTRATION] KEY-WORD: ");
                    var keyword = Console.ReadLine();

                    Console.Write("REGISTRATION] GENDER: ");
                    var gender = Console.ReadLine();
                    if (gender.ToLower() != "male" && gender.ToLower() != "female" && gender.ToLower() != "other")
                        throw new RegistrationException("[ERROR] Gender is not correct!");

                    Console.Write("REGISTRATION] Email: ");
                    var email = Console.ReadLine();
                    if (!email.ToLower().Contains(".") && !email.ToLower().Contains("@"))
                        throw new RegistrationException("[ERROR] Email is not correct!");

                    Console.Write("REGISTRATION] TELEPHONE NUMBER: ");
                    var telephone = Console.ReadLine();
                    if (telephone.Any(char.IsLetter))
                    {
                        throw new RegistrationException("[ERROR] Telephone number is not correct!");
                    }

                    Console.WriteLine();
                    Console.Write("[REGISTRATION] CARD NUMBER: ");
                    var cardNumber = Convert.ToInt64(Console.ReadLine());

                    Console.Write("[REGISTRATION] DATE IN FORMAT (XX/YY/DDDD): ");
                    var cardDate = Console.ReadLine();
                    if (!cardDate.Contains("/") && cardDate.Length != 10)
                        throw new RegistrationException("[ERROR] Card date is not correct!");

                    Console.Write("[REGISTRATION] CVV IN FORM (XXX-digits): ");
                    var cardCVV = Convert.ToInt32(Console.ReadLine());
                    if (cardCVV < 100 || cardCVV > 999)
                        throw new RegistrationException("[ERROR] CVV is not correct!");

                    var user = new User
                    {
                        Name = name,
                        Surname = surname,
                        Login = login,
                        Password = password,
                        KeyWord = keyword,
                        Gender = gender,
                        Email = email,
                        TelephoneNumber = telephone,
                        Card = new BankCard
                        {
                            Number = cardNumber,
                            Date = cardDate,
                            CVV = cardCVV
                        }
                    };

                    await loginService.RegisterAsync(user);

                    Console.WriteLine();
                    ConsoleSettingsChanger.DisplayGreenMessage("[SUCCESS] USER HAS BEEN SUCCESSFULLY REGISTRATED!");
                    break;
                }

                catch (RegistrationException e)
                {
                    ConsoleSettingsChanger.DisplayRedMessage("\n\n<--------FATAL ERROR--------->");
                    ConsoleSettingsChanger.DisplayRedMessage(e.Message);

                    Thread.Sleep(5000);
                }
                catch
                {
                    ConsoleSettingsChanger.DisplayRedMessage("\n\n<--------FATAL ERROR--------->");
                    ConsoleSettingsChanger.DisplayRedMessage("[ERROR] Some field is not correct!");
                    Thread.Sleep(5000);
                }
            }

            Thread.Sleep(5000);
            await ShowAuth();
        }

        public async Task LogIn()
        {
            Console.Clear();

            ConsoleSettingsChanger.DisplayGreenMessage("<-----LOG IN----->");

            Console.Write("[LOG-IN] Your login: ");
            var login = Console.ReadLine();

            Console.Write("[LOG-IN] Your password: ");
            var password = Console.ReadLine();

            currentUser = await loginService.AuthorizateAsync(login, password);

            if (currentUser != null)
            {
                ConsoleSettingsChanger.DisplayGreenMessage("\n[SUCCESS] You have successfully logged in :)");
                Thread.Sleep(5000);
            }
            else
            {
                ConsoleSettingsChanger.DisplayRedMessage("\n[ERROR] User with this login and password was not found in database! Please registrate!");
                Thread.Sleep(5000);
                await ShowAuth();
            }
        }
    }
}
