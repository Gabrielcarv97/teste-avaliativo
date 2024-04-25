using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        private int numero;
        private string titular;
        private double depositoInicial;

        public ContaBancaria(int numero, string titular)
        {
            this.numero = numero;
            this.titular = titular;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            this.numero = numero;
            this.titular = titular;
            this.depositoInicial = depositoInicial;
        }

        internal void Deposito(double quantia)
        {
            depositoInicial = depositoInicial + quantia;  
        }

        internal void Saque(double quantia)
        {
            depositoInicial = (depositoInicial - quantia) - 3.50;
        }

        public override string ToString()
        {
            return $"Número: {numero}, Titular: {titular}, Saldo: R$ {this.depositoInicial.ToString("F2", CultureInfo.InvariantCulture)}";
        }

    }
}
