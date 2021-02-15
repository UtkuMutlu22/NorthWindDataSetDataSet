
using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.Business.DependencyResolvers.Ninject;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>();
            _categoryService =InstanceFactory.GetInstance<ICategoryService>();
        }

        private IProductService _productService;
        private ICategoryService _categoryService;
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();
        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";

            cbxCategory2.DataSource = _categoryService.GetAll();
            cbxCategory2.DisplayMember = "CategoryName";
            cbxCategory2.ValueMember = "CategoryId";

            cbxCategoryUpdate.DataSource = _categoryService.GetAll();
            cbxCategoryUpdate.DisplayMember = "CategoryName";
            cbxCategoryUpdate.ValueMember = "CategoryId";

            cbxCategoryDelete.DataSource = _categoryService.GetAll();
            cbxCategoryDelete.DisplayMember = "CategoryName";
            cbxCategoryDelete.ValueMember = "CategoryId";
        }

        private void LoadProducts()
        {
            dgwProduct.DataSource = _productService.GetAll();
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productService.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch
            {


            }
        }

        private void tbxProduct_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxProduct.Text))
            {
                dgwProduct.DataSource = _productService.GetProductsByProductName(tbxProduct.Text);
            }
            else
            {
                LoadProducts();
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _productService.Add(new Product
                {

                    CategoryId = Convert.ToInt32(cbxCategory2.SelectedValue),
                    ProductName = tbxProductName2.Text,
                    QuantityPerUnit = tbxQuantityPerUnit.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                    UnitsInStock = (short)Convert.ToUInt16(tbxStock.Text)

                });
                MessageBox.Show("Ürün Kayıt edildi");
                LoadProducts();
            }
            catch (Exception excepsion)
            {

                MessageBox.Show(excepsion.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productService.Update(new Product
            {
                ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                CategoryId = Convert.ToInt32(cbxCategoryUpdate.SelectedValue),
                ProductName = tbxProductUpdate.Text,
                QuantityPerUnit = tbxQuantityPerUnitUpdate.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                UnitsInStock = (short)Convert.ToUInt16(tbxStockAmountUpdate.Text)
            }) ;
            MessageBox.Show("Ürün Güncellendi");
            LoadProducts();
        }

        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxProductUpdate.Text = dgwProduct.CurrentRow.Cells[1].Value.ToString();
            cbxCategoryUpdate.SelectedValue = dgwProduct.CurrentRow.Cells[2].Value;
            tbxUnitPriceUpdate.Text = dgwProduct.CurrentRow.Cells[3].Value.ToString();
            tbxStockAmountUpdate.Text = dgwProduct.CurrentRow.Cells[4].Value.ToString();
            tbxQuantityPerUnitUpdate.Text = dgwProduct.CurrentRow.Cells[5].Value.ToString();

            tbxProductDelete.Text = dgwProduct.CurrentRow.Cells[1].Value.ToString();
            cbxCategoryDelete.SelectedValue = dgwProduct.CurrentRow.Cells[2].Value;
            tbxUnitPriceDelete.Text = dgwProduct.CurrentRow.Cells[3].Value.ToString();
            tbxStockAmountDelete.Text = dgwProduct.CurrentRow.Cells[4].Value.ToString();
            tbxQuantityPerUnitDelete.Text = dgwProduct.CurrentRow.Cells[5].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _productService.Delete(new Product
                {
                    ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value)

                });
                MessageBox.Show("Ürün Silindi");
                LoadProducts();
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
                
            
            
        }
    }
}
