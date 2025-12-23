using EmployeeApp.Core.Strategies;

public static class BankRegistry
{
    public static List<IBankService> Banks { get; } = new List<IBankService>
    {
        new CommissionBankService("Сбербанк", 1.0),
        new CommissionBankService("Газпромбанк", 1.5),
        new CommissionBankService("Тинькофф", 2.0),
        new CommissionBankService("ВТБ", 0.9)
    };

    public static IBankService GetDefault() => Banks[0];
}