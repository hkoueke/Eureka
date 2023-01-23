using ErrorOr;

namespace EurekaBot.Domain.Errors;

public partial class Errors
{
    public static class Document
    {
        public static readonly Error AlreadyExists = Error.Conflict(
           "Document.AlreadyExists",
           "Document already exists.");

        public static readonly Error InvalidIdentifier = Error.Validation(
            "Document.InvalidIdentifier",
            "Document identifier is invalid.");

        public static readonly Error EmptyIdentifier = Error.Validation(
            "Document.EmptyIdentifier",
            "Document identifier is empty.");

        public static readonly Error IdentifierTooLong = Error.Validation(
            "Document.IdentifierTooLong",
            "Document identifier is too long.");

        public static readonly Error IdentifierTooShort = Error.Validation(
            "Document.IdentifierTooShort",
            "Document identifier is too short.");

        public static readonly Error NameTooLong = Error.Validation(
            "Document.NameTooLong",
            "Document owner name is too long.");

        public static readonly Error NameTooShort = Error.Validation(
            "Document.NameTooShort",
            "Document owner name is too short.");

        public static readonly Error EmptyName = Error.Validation(
            "Document.EmptyName",
            "Document owner name is empty.");

        public static readonly Error DescriptionTooLong = Error.Validation(
            "Document.DescriptionTooLong",
            "Document description is too long.");

        public static readonly Error DescriptionTooShort = Error.Validation(
            "Document.DescriptionTooShort",
            "Document description is too short.");

        public static readonly Error EmptyDescription = Error.Validation(
            "Document.EmptyDescription",
            "Document description is empty.");
    }
}