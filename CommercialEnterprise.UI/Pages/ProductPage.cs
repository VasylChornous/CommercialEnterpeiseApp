using CommercialEnterprise.Core.Exceptions;
using CommercialEnterprise.Infrastructure.Models;
using CommercialEnterprise.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommercialEnterprise.UI
{
    public class ProductPage
    {
        private readonly IProductRepository productService;
        private User currentUser;

        public ProductPage(IProductRepository productService, User currentUser)
        {
            this.productService = productService;
            this.currentUser = currentUser;
        }

        public async Task ShowMain()
        {
            Console.Clear();
            ConsoleSettingsChanger.DisplayGreenMessage($"<<< USER INFO: You logged in as {currentUser.Name} {currentUser.Surname}! >>>\n");

            ConsoleSettingsChanger.DisplayRedMessage("<0> - CLOSE APP;");
            ConsoleSettingsChanger.DisplayGreenMessage("<1> - SHOW ALL PRODUCTS;");
            ConsoleSettingsChanger.DisplayGreenMessage("<2> - ADD PRODUCT;");
            ConsoleSettingsChanger.DisplayGreenMessage("<3> - DELETE PRODUCT;");
            ConsoleSettingsChanger.DisplayGreenMessage("<4> - UPDATE PRODUCT;");

            Console.Write("\nYour choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    await ShowProducts();
                    break;
                case "2":
                    await AddProduct();
                    break;
                case "3":
                    await DeleteProduct();
                    break;
                case "4":
                    await UpdateProduct();
                    break;
                default:
                    break;
            }
        }

        public async Task DeleteProduct()
        {
            Console.Clear();
            ConsoleSettingsChanger.DisplayGreenMessage($"<<<DELETING PRODUCT FOR {currentUser.Name} {currentUser.Surname} >>>\n");
            ConsoleSettingsChanger.DisplayRedMessage("-------------------------------------------------");
            Console.WriteLine();

            try
            {
                Console.Write("[DELETE] ID of product: ");
                var id = Convert.ToInt32(Console.ReadLine());
                if (!await productService.IsProductExistsByIdAsync(id))
                {
                    throw new ProductException($"[ERROR] Product with ID '{id}' is not exist!");
                }
                await productService.DeleteProductByIdAsync(id);

                Console.WriteLine();
                ConsoleSettingsChanger.DisplayGreenMessage($"[SUCCESS] Product with ID {id} has been deleted :)");
            }
            catch (UserException e)
            {
                ConsoleSettingsChanger.DisplayRedMessage(e.Message);
            }

            Thread.Sleep(5000);
            await ShowMain();
        }

        public async Task UpdateProduct()
        {
            Console.Clear();
            ConsoleSettingsChanger.DisplayGreenMessage($"<<<UPDATING PRODUCT FOR {currentUser.Name} {currentUser.Surname} >>>\n");
            ConsoleSettingsChanger.DisplayRedMessage("-------------------------------------------------");
            Console.WriteLine();

            try
            {
                Console.Write("[UPDATE] ID of updated product: ");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                Console.Write("[PRODUCT] Name: ");
                var name = Console.ReadLine();

                Console.Write("[PRODUCT] Quantity: ");
                var quantity = Convert.ToInt32(Console.ReadLine());

                Console.Write("[PRODUCT] Price: ");
                var price = Convert.ToDouble(Console.ReadLine());

                Console.Write("[PRODUCT] Description: ");
                var desc = Console.ReadLine();

                var product = new Product
                {
                    Id = id,
                    Name = name,
                    Quantity = quantity,
                    Price = price,
                    Description = desc,
                    UserId = currentUser.Id
                };
                await productService.UpdateProductAsync(product);


                Console.WriteLine();
                ConsoleSettingsChanger.DisplayGreenMessage($"[SUCCESS] Product with ID {id} has been updated :)");
            }
            catch (UserException e)
            {
                ConsoleSettingsChanger.DisplayRedMessage(e.Message);
            }

            Thread.Sleep(5000);
            await ShowMain();

        }
        public async Task ShowProducts()
        {
            Console.Clear();
            ConsoleSettingsChanger.DisplayGreenMessage($"<<< ALL PRODUCTS FOR USER {currentUser.Name} {currentUser.Surname} >>>\n");

            var products = await productService.GetAllByUserIdAsync(currentUser.Id);

            ConsoleSettingsChanger.DisplayGreenMessage("-----------------------------------------");
            foreach (var product in products)
            {
                Console.WriteLine($"[PRODUCT] ID = {product.Id}");
                Console.WriteLine($"[PRODUCT] Name = {product.Name}");
                Console.WriteLine($"[PRODUCT] Quantity = {product.Quantity}");
                Console.WriteLine($"[PRODUCT] Price = {product.Price}");
                Console.WriteLine($"[PRODUCT] Description = {product.Description}");
                ConsoleSettingsChanger.DisplayGreenMessage("-----------------------------------------");
            }

            ConsoleSettingsChanger.DisplayRedMessage("\nIf you want to back enter some key: ");
            Console.ReadLine();

            await ShowMain();
        }

        public async Task AddProduct()
        {
            Console.Clear();
            ConsoleSettingsChanger.DisplayGreenMessage($"<<<CREATING PRODUCT FOR {currentUser.Name} {currentUser.Surname} >>>\n");

            try
            {
                Console.Write("[PRODUCT] Name: ");
                var name = Console.ReadLine();

                Console.Write("[PRODUCT] Quantity: ");
                var quantity = Convert.ToInt32(Console.ReadLine());

                Console.Write("[PRODUCT] Price: ");
                var price = Convert.ToDouble(Console.ReadLine());

                Console.Write("[PRODUCT] Description: ");
                var desc = Console.ReadLine();

                await productService.AddProductAsync(new Product
                {
                    Name = name,
                    Quantity = quantity,
                    Price = price,
                    Description = desc,
                    UserId = currentUser.Id
                });

                Console.WriteLine();
                ConsoleSettingsChanger.DisplayGreenMessage("[SUCCESS] Product has been added :)");
                Thread.Sleep(5000);

                await ShowMain();
            }
            catch
            {
                ConsoleSettingsChanger.DisplayRedMessage("\n[ERROR] Incorrect input!");
                Thread.Sleep(5000);

                await AddProduct();
            }
        }
    }
}
