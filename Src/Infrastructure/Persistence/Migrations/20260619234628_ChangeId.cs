using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_Categoriesid",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Products_Productsid",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_Productid",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_Brandid",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Brandid",
                table: "Products",
                newName: "BrandId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Brandid",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.RenameColumn(
                name: "Productid",
                table: "Images",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Images",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Images_Productid",
                table: "Images",
                newName: "IX_Images_ProductId");

            migrationBuilder.RenameColumn(
                name: "Productsid",
                table: "CategoryProduct",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "Categoriesid",
                table: "CategoryProduct",
                newName: "CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryProduct_Productsid",
                table: "CategoryProduct",
                newName: "IX_CategoryProduct_ProductsId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Brands",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Products",
                newName: "Brandid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                newName: "IX_Products_Brandid");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Images",
                newName: "Productid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Images",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                newName: "IX_Images_Productid");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "CategoryProduct",
                newName: "Productsid");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "CategoryProduct",
                newName: "Categoriesid");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct",
                newName: "IX_CategoryProduct_Productsid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Brands",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_Categoriesid",
                table: "CategoryProduct",
                column: "Categoriesid",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Products_Productsid",
                table: "CategoryProduct",
                column: "Productsid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_Productid",
                table: "Images",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_Brandid",
                table: "Products",
                column: "Brandid",
                principalTable: "Brands",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
