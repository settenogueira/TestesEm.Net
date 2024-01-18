using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Alura.Estacionamento.Tests
{
    public class PatioTeste:IDisposable
    {

        private Veiculo veiculo = new Veiculo();
        private Operador operador;
        private Patio estacionamento;

        public ITestOutputHelper Output { get; }
        public PatioTeste(ITestOutputHelper output)
        {
            Output = output;
            Output.WriteLine("Execução do Construtor");

            veiculo.Proprietario = "André Silva";
            veiculo.Placa = "ASD-9999";
            veiculo.Cor = "Preto";
            veiculo.Modelo = "Fusca";

            operador = new Operador();

            estacionamento = new Patio();
            estacionamento.OperadorPatio = operador;

        }

        [Fact]
        public void ValidaFaturamentoDoEstacionamentoComUmVeiculo()
        {
            //Arrange
            //Patio estacionamento = new Patio();
            //var veiculo = new Veiculo();
            //veiculo.Proprietario = "André Silva";
            //veiculo.Placa = "ASD-9999";
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("ASD-9631","André Silva","Fusca","Preto")]
        [InlineData("WER-8754", "José Silva", "Uno", "Amarelo")]
        [InlineData("TRD-7418", "Maria Silva", "Gol", "Vermelho")]
        public void ValidaFaturamentoParaVariosVeiculosNoEstacionamento(string placa,string proprietario,string modelo, string cor)
        {
            //Arrange
            //Patio estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Placa = placa;
            veiculo.Proprietario = proprietario;
            veiculo.Modelo = modelo;
            veiculo.Cor = cor;
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);
            
            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("André Silva","ASD-1234","Verde","Fusca")]
        public void LocalizaUmVeiculoNoEstacionamentoComBaseNaPlaca(string proprietario,
                                    string placa,
                                    string cor,
                                    string modelo)
        {
            //Arrange
           // Patio estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculo(veiculo.IdTicket);

            //Assert
            Assert.Equal(veiculo.Placa, consultado.Placa);

        }

        public void Dispose()
        {
            Output.WriteLine("Execução do Cleanup");
        }
    }
}
