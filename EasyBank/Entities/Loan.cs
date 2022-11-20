﻿using EasyBank.Crosscutting;

namespace EasyBank.Entities
{
    public class Loan : BaseEntity
    {
        public int OwnerID { get; set; }
        public double Value { get; set; }
        public int Parcels { get; set; }
        public bool Open { get; set; }

        public Loan(double _value, int _parcels, int ownerID, int _id, bool _open)
        {
            Value = _value;
            Parcels = _parcels;
            OwnerID = ownerID;
            Id = _id;
            Open = _open;
        }
        public Loan()
        {

        }
        public void LoanRequest(List<User> users, List<Loan> loans, List<Bill> bills, int userID)
        {
            var user = users.Find(x => x.Id == userID);

            if (user.OpenLoan == true)
            {
                Message.ErrorGeneric("Não é possivel abrir mais de um empréstimo");
            }

            else
            {
                Console.Write("Digite a quantia: ");
                var loanValue = Convert.ToInt32(Console.ReadLine());

                var twoYearsSalary = user.MonthlyIncome * 24;

                if (loanValue > twoYearsSalary)
                {
                    Message.ErrorGeneric("Quantia não disponivel para você");
                }

                else
                {
                    PaymentOption(loanValue, bills, loans, userID, users);
                }
            }
        }
        public void PaymentOption(int loanValue, List<Bill> bills, List<Loan> loans, int userID, List<User> users)
        {
            var paymentOptions = "Crédito";

            Console.WriteLine("Forma de pagamento permitida");
            Console.WriteLine("Credíto até 12x - MasterCard\nDigite 1 para continuar\nDigite: ");
            var userInputChoice = Console.ReadLine();

            if (userInputChoice == "1")
                ChooseQtdParcels(loanValue, paymentOptions, bills, loans, userID, users);
        }
        public double AmountInterest(int value)
        {
            // gerador automatico de calculo para juros

            /*Random random = new();
            var percentual = random.Next(10);
            var calculator = random.Next(8);

            var amount = (value / percentual) * calculator;
            */

            var standardCalculate = 3;
            var percentualBase = 2;

            var finalValue = value / percentualBase * standardCalculate;

            return finalValue;
        }
        public void ChooseQtdParcels(int loanValue, string paymentOptions, List<Bill> bills, List<Loan> loans, int userID, List<User> users)
        {
            Console.Write("Digite o numero de parcelas: ");
            var qtdParcels = Convert.ToInt32(Console.ReadLine());

            if (qtdParcels > 12 || qtdParcels < 1)
            {
                Message.ErrorGeneric("Escolha indisponivel");
            }

            else
            {
                var finalInterestValue = AmountInterest(loanValue);

                var finalValue = loanValue + finalInterestValue;

                Console.WriteLine($"Valor do emprestimo: {loanValue}\nForma de Pagamento: {paymentOptions}\n" +
                    $"Parcelas: {qtdParcels}\nValor Parcela:{finalValue / qtdParcels}\n" +
                    $"Juros: {finalInterestValue}\nTotal: {loanValue + finalInterestValue}");


                if (ConfirmLoan() == true)
                    ApplyLoan(bills, loans, qtdParcels, finalValue, userID, users);

                else
                {
                    Console.WriteLine("Empréstimo cancelado");
                }
            }
        }
        public bool ConfirmLoan()
        {
            Console.Write("1 - Confirmar\n2 - Cancelar\nDigite: ");
            var Choice = Console.ReadLine();
            if (Choice == "1")
                return true;

            return false;
        }
        public void ApplyLoan(List<Bill> bills, List<Loan> loans, int qtdParcels, double finalValue, int userID, List<User> users)
        {
            var loan = new Loan(finalValue, qtdParcels, userID, UserValidator.ID_AUTOINCREMENT(loans), true);
            loans.Add(loan);

            var user = users.Find(x => x.Id == userID).OpenLoan = true;

            bills.Add(new Bill
            {
                Name = "EMPRÉSTIMO",
                NumberParcels = qtdParcels,
                OwnerID = userID,
                Value = finalValue,
                Id = UserValidator.ID_AUTOINCREMENT(bills),
                ValueParcel = finalValue / qtdParcels,
            });
        }
    }
}