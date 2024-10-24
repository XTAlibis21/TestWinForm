using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWinForm
{
    public partial class Form1 : Form
    {
        private readonly Car_RentalEntities1 car_RentalEntities1;
        public Form1()
        {
            InitializeComponent();
            car_RentalEntities1 = new Car_RentalEntities1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = tbCustomerName.Text;
                var dateOut = dtRented.Value;
                var dateIn = dtReturned.Value;
                double cost = Convert.ToDouble(tbCost.Text);

                var carType = cbTypeOfCar.Text;
                var isValid = true;
                var errorMessage = "";

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(carType))
                {
                    isValid = false;
                    errorMessage += "Error: Please Enter Missing data.\n\r";
                }

                if (dateOut > dateIn)
                {
                    isValid = false;
                    errorMessage += "Error: Illegal Date Selection\n\r";
                }


                if (isValid)
                {
                    var rentalRecord = new CarRentRecord();
                    rentalRecord.CustomerName = customerName;
                    rentalRecord.DateRented = dateOut;
                    rentalRecord.DateReturned = dateIn;
                    rentalRecord.Cost = (decimal)cost;
                    rentalRecord.TypeOfCarid = (int)cbTypeOfCar.SelectedValue;

                    car_RentalEntities1.CarRentRecords.Add(rentalRecord);
                    car_RentalEntities1.SaveChanges();

                    MessageBox.Show($"Customer Name:{customerName}\n\r" +
                        $"Date Rented: {dateOut}\n\r" +
                        $"Date Returned: {dateIn}\n\r" +
                        $"Cost: {tbCost}\n\r" +
                        $"Car Type: {carType}\n\r" +
                        $"Thank you for Bussiness");
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //select from TypeOfCars
            var cars = car_RentalEntities1.TypeOfCars.ToList();
            cbTypeOfCar.DisplayMember = "Name";
            cbTypeOfCar.ValueMember = "id";
            cbTypeOfCar.DataSource = cars;
        }
    }
}    


