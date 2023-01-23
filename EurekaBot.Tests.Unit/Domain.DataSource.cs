using System.Collections.Generic;

namespace EurekaBot.Tests.Unit;

public partial class DataSource
{
    public static IEnumerable<object[]> GetCreditCardNumbers()
    {
        yield return new object[] { " ", true };
        yield return new object[] { "1234 5678 9012 3456", true };
        yield return new object[] { "123456789012345", true };
        yield return new object[] { "4325credit012345", true };
        yield return new object[] { "731290246420", false };
        yield return new object[] { "4325 7312 9024 6420", false };
    }

    public static IEnumerable<object[]> GetIsoCodes()
    {
        yield return new object[] { " ", true };
        yield return new object[] { "a ", true };
        yield return new object[] { "aab", true };
        yield return new object[] { "cm", false };
    }

    public static IEnumerable<object[]> GetIdentifiers()
    {
        yield return new object[] { " ", true };
        yield return new object[] { "b", true };
        yield return new object[] { "b ", true };
        yield return new object[] { "bb", true };
        yield return new object[] { "bb ", true };
        yield return new object[] { "aaaaaaaaaaaaaaaa", true };
        yield return new object[] { "aaaaaaaaaaaaaaa", false };
    }

    public static IEnumerable<object[]> GetFullNames()
    {
        yield return new object[] { " ", true };
        yield return new object[] { "b", true };
        yield return new object[] { "b ", true };
        yield return new object[] { "bb", true };
        yield return new object[] { "bb ", true };
        yield return new object[] { "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", true };
        yield return new object[] { "gates", false };
    }
}
