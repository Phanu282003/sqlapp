using Microsoft.FeatureManagement;
using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{

    // This service will interact with our Product data in the SQL database
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private readonly IFeatureManager _featureManager;


        public ProductService(IConfiguration configuration, IFeatureManager featureManager)
        {
            _configuration = configuration;
            _featureManager = featureManager;
        }
        private SqlConnection GetConnection()
        {
            // This line get connection string from Configuration in WebApp
            //return new SqlConnection(_configuration.GetConnectionString("SQLConnection")); // This line get connectionString from 
            
            // This line get connection string from Azure configuration  
            return new SqlConnection(_configuration["SQLConnection"]);
        }

        public async Task<bool> IsBeta()
        {
            if(await _featureManager.IsEnabledAsync("beta"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Product> GetProducts()
        {
            List<Product> _product_lst = new List<Product>();
            string _statement = "SELECT ProductID,ProductName,Quantity from Products";
            SqlConnection _connection = GetConnection();

            _connection.Open();

            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = _reader.GetInt32(0),
                        ProductName = _reader.GetString(1),
                        Quantity = _reader.GetInt32(2)
                    };

                    _product_lst.Add(_product);
                }
            }
            _connection.Close();
            return _product_lst;
        }

    }
}

