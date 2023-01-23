using EurekaBot.Domain.Services;
using FluentAssertions;

namespace EurekaBot.Tests.Unit.Domain.Validators;

public class ParserValidatorsTests
{
    [Theory]
    [MemberData(nameof(DataSource.GetIsoCodes), MemberType = typeof(DataSource))]
    public void Parser_ShouldValidate_GivenIsoCodes(string inputValue, bool expected)
    {
        //Arrange
        //Act
        var result = Parser.Parse(inputValue, Predicates.IsoCodePredicate);

        //Assert
        result.IsError.Should().Be(expected, result.FirstError.Description);

        if (expected) result.Value.Should().BeNull();
        else
        {
            result.Value.Should().NotBeNullOrEmpty();
            result.Value.Should().BeUpperCased();
        }
    }

    [Theory]
    [MemberData(nameof(DataSource.GetIdentifiers), MemberType = typeof(DataSource))]
    public void Parser_ShouldValidate_GivenIdentifiers(string inputValue, bool expected)
    {
        //Arrange
        //Act
        var result = Parser.Parse(inputValue, Predicates.IdentifierPredicate);

        //Assert
        result.IsError.Should().Be(expected, result.FirstError.Description);

        if (expected) result.Value.Should().BeNull();
        else
        {
            result.Value.Should().NotBeNullOrEmpty();
            result.Value.Should().BeUpperCased();
        }
    }

    [Theory]
    [MemberData(nameof(DataSource.GetFullNames), MemberType = typeof(DataSource))]
    public void Parser_ShouldValidate_GivenFullNames(string inputValue, bool expected)
    {
        //Arrange
        //Act
        var result =
            Parser.Parse(inputValue, Predicates.FullNamePredicate);

        //Assert
        result.IsError.Should().Be(expected, result.FirstError.Description);

        if (expected) result.Value.Should().BeNull();
        else
        {
            result.Value.Should().NotBeNullOrEmpty();
            result.Value.Should().BeUpperCased();
        }
    }

    [Theory]
    [MemberData(nameof(DataSource.GetCreditCardNumbers), MemberType = typeof(DataSource))]
    public void Parser_ShouldValidate_GivenCreditCardNumbers(string inputValue, bool expected)
    {
        //Arrange
        //Act
        var result = Parser.Parse(inputValue, Predicates.CreditCardNumberPredicate);

        //Assert
        result.IsError.Should().Be(expected, result.FirstError.Description);

        if (expected) result.Value.Should().BeNull();
        else result.Value.Should().NotBeNullOrEmpty();
    }
}
