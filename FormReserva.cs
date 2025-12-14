using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Builder;
using Facade;
using Models;
using Observers;
using Repositories;
using Strategy;

namespace SimulacroFinal
{
    public partial class FormReserva : Form, IReservaObserver
    {
        private ReservaFacade _facade;

        public FormReserva()
        {
            InitializeComponent();
            this.Load += FormReserva_Load; // conecto el evento load con el constructor

            var builder = new ReservaBuilder();
            var service = new ReservaService();
            var repo = new RepositorioJson<Reserva>("reservas.json");

            _facade = new ReservaFacade(builder, service, repo);

            // (si usás observer simple)
            service.Suscribir(this); // si el Form implementa IReservaObserver

            var listaInicial = _facade.ListarReservas().ToList(); // para mostrar los datos cargados en JSON 
            dgvReservas.DataSource = listaInicial;
        }
        public void OnReservaConfirmada(Reserva reserva) //implemento IReservaObserver
        {
            MessageBox.Show(
                $"Reserva confirmada para {reserva.Viajero}\n" +
                $"Total: ${reserva.Total}",
                "Reserva",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            var lista = _facade.ListarReservas().ToList();

            dgvReservas.AutoGenerateColumns = true;  // clave
            dgvReservas.DataSource = null;
            dgvReservas.DataSource = lista;
          
        }



        private void dgvReservas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregarDestino_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = textDestino.Text;
                decimal precio = decimal.Parse(textPrecio.Text);
                int dias = int.Parse(textDias.Text);

                _facade.AgregarDestino(nombre, precio, dias);

                MessageBox.Show("Destino agregado correctamente");

                // 🔹 LIMPIAR CAMPOS
                textDestino.Clear();
                textPrecio.Clear();
                textDias.Clear();
                textDestino.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTransporte.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un transporte");
                    return;
                }

                switch (cmbTransporte.SelectedItem.ToString())
                {
                    case "Avión":
                        _facade.SeleccionarTransporte(new TransporteAvion());
                        break;

                    case "Micro":
                        _facade.SeleccionarTransporte(new TransporteMicro());
                        break;

                    case "Auto":
                        _facade.SeleccionarTransporte(new TransporteAuto());
                        break;
                }

                
                string viajero = textViajero.Text;
                DateTime fecha = dtpFecha.Value;

                _facade.ConfirmarReserva(viajero, fecha);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }



        private void cmbTransporte_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormReserva_Load(object sender, EventArgs e)
        {
            cmbTransporte.Items.Clear();
            cmbTransporte.Items.AddRange(new object[]
            {
              "Avión",
              "Micro",
              "Auto"
            });
            cmbTransporte.SelectedIndex = -1;


        }
    }
}
