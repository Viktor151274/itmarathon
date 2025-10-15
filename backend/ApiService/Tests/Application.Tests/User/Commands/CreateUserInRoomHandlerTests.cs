using Bogus;
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.UserCases.Commands;
using Epam.ItMarathon.ApiService.Application.UseCases.UserCases.Handlers;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentAssertions;
using FluentValidation.Results;
using NSubstitute;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Epam.ItMarathon.ApiService.Application.Tests.User.Commands
{
    /// <summary>
    /// Unit tests for the <see cref="CreateUserInRoomHandler"/> class.
    /// </summary>
    public class CreateUserInRoomHandlerTests
    {
        private static readonly Faker DataFaker = new();
        private readonly IRoomRepository _roomRepositoryMock;
        private readonly IUserReadOnlyRepository _userReadOnlyRepositoryMock;
        private readonly CreateUserInRoomHandler _handler;

        /// <summary>
        /// Generates a TheoryData object containing a random string of the specified length.
        /// </summary>
        /// <param name="stringLength">Length of the string to generate.</param>
        public static TheoryData<string> GetRandomString(int stringLength) => [DataFaker.Random.String(stringLength)];

        /// <summary>
        /// Generates a TheoryData object containing invalid wish list scenarios.
        /// </summary>
        public static TheoryData<bool, IEnumerable<(string?, string?)>> InvalidWishes => new()
        {
            { false, [] },
            { false, [("Same", null), ("Same", null)] },
            { true, [(DataFaker.Random.String(10), null)] },
        };

        /// <summary>
        /// Generates a TheoryData object containing various invalid email formats.
        /// </summary>
        public static TheoryData<string> InvalidEmails =>
        [
            "not_valid_email.com", // Missing @ symbol
            "missingdomain@", // Missing domain name
            "@missingusername.com", // Missing username
            "username..dots@example.com", // Consecutive dots
            "username@-domain.com", // Domain starts with a hyphen
            "username@domain-.com", // Domain ends with a hyphen
            new string('a', 65) + "@example.com", // Username exceeds 64 characters
            "username@" + new string('a', 64) + ".com", // Domain exceeds 253 characters
            "user name@example.com", // Email with spaces
            "user\"name@example.com", // Double quotes not properly escaped
            "username@domain_with_underscore.com" // Invalid character in domain
        ];

        /// <summary>
        /// Generates a TheoryData object containing various invalid phone number formats.
        /// </summary>
        public static TheoryData<string> InvalidPhoneNumbers =>
        [
            "380123456789", // Missing '+'
            "+381123456789", // Incorrect country code
            "+38012345678", // Too few digits
            "+3801234567890", // Too many digits
            "+38012345678a", // Contains a letter
            "+38012345@789", // Contains a special character
            "+380", // Only country code
            "+380123", // Partial digits
            "+380 123456789", // Contains a space
            "+380123 456 789", // Multiple spaces
            "+38-012-345-67-89", // Contains dashes
            "+380.123.456.789", // Contains dots
            "", // Empty string
            null
        ];

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserInRoomHandlerTests"/> class with mocked dependencies.
        /// </summary>
        public CreateUserInRoomHandlerTests()
        {
            _roomRepositoryMock = Substitute.For<IRoomRepository>();
            _userReadOnlyRepositoryMock = Substitute.For<IUserReadOnlyRepository>();
            _handler = new CreateUserInRoomHandler(_roomRepositoryMock, _userReadOnlyRepositoryMock);
        }

        /// <summary>
        /// Tests that the handler returns a NotFoundError when the specified room is not found.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoomNotFound()
        {
            // Arrange
            var fakeUser = DataFakers.UserApplicationFaker.Generate();
            var request = new CreateUserInRoomRequest(fakeUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(new NotFoundError([
                    new ValidationFailure("code", string.Empty)
                ]));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("code"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid first name.
        /// </summary>
        /// <param name="firstName">User's first name to test.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [MemberData(nameof(GetRandomString), 41)]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidFirstName(string? firstName)
        {
            // Arrange
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.FirstName, _ => firstName)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(Result.Success<Room, ValidationResult>(existingRoom));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].firstName"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid дфіе name.
        /// </summary>
        /// <param name="lastName">User's last name to test.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [MemberData(nameof(GetRandomString), 41)]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidLastName(string? lastName)
        {
            // Arrange
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.LastName, _ => lastName)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(Result.Success<Room, ValidationResult>(existingRoom));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].lastName"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid delivery info.
        /// </summary>
        /// <param name="deliveryInfo">User's delivery info to test.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [MemberData(nameof(GetRandomString), 501)]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidDeliveryInfo(string? deliveryInfo)
        {
            // Arrange
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.DeliveryInfo, _ => deliveryInfo)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(Result.Success<Room, ValidationResult>(existingRoom));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].deliveryInfo"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid phone number.
        /// </summary>
        /// <param name="phoneNumber">User's phone number to test.</param>
        [Theory]
        [MemberData(nameof(InvalidPhoneNumbers))]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidPhone(string? phoneNumber)
        {
            // Arrange
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.Phone, _ => phoneNumber)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(Result.Success<Room, ValidationResult>(existingRoom));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].phone"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid email.
        /// </summary>
        /// <param name="email">User's email to test.</param>
        [Theory]
        [MemberData(nameof(InvalidEmails))]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidEmail(string email)
        {
            // Arrange
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.Email, _ => email)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(Result.Success<Room, ValidationResult>(existingRoom));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].email"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid wishes based on their wantSurprise preference.
        /// </summary>
        /// <param name="wantSurprise">User's want surprise preference.</param>
        /// <param name="wishList">User's wish list to test.</param>
        [Theory]
        [MemberData(nameof(InvalidWishes))]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidWishes(bool wantSurprise,
            IEnumerable<(string?, string?)> wishList)
        {
            // Arrange
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.WantSurprise, _ => wantSurprise)
                .RuleFor(user => user.Interests, _ => null)
                .RuleFor(user => user.Wishes, _ => wishList)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(Result.Success<Room, ValidationResult>(existingRoom));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].wishList"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has invalid interests based on their wantSurprise preference.
        /// </summary>
        /// <param name="wantSurprise">User's want surprise preference.</param>
        /// <param name="interests">User's interests to test.</param>
        [Theory]
        [InlineData(true, null)]
        [InlineData(false, "text")]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidInsterests(bool wantSurprise, string? interests)
        {
            // Arrange
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.WantSurprise, _ => wantSurprise)
                .RuleFor(user => user.Interests, _ => interests)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(Result.Success<Room, ValidationResult>(existingRoom));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].interests"));
        }

        /// <summary>
        /// Tests that the handler successfully creates a user when provided with valid user information.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldCreateUserSuccessfully_WhenProvidedValidUserInfo()
        {
            // Arrange
            var validUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.FirstName, faker => faker.Random.String(40))
                .RuleFor(user => user.LastName, faker => faker.Random.String(40))
                .RuleFor(user => user.Email, faker => faker.Internet.Email())
                .RuleFor(user => user.Phone, faker => faker.Phone.PhoneNumber("+380#########"))
                .RuleFor(user => user.DeliveryInfo, faker => faker.Random.String(500))
                .RuleFor(user => user.WantSurprise, _ => false)
                .RuleFor(user => user.Interests, _ => null)
                .RuleFor(user => user.Wishes, faker =>
                [
                    (faker.Random.String(10), null),
                    (faker.Random.String(10), faker.Internet.Url().Replace("http:", "https:"))
                ])
                .Generate();
            var existingRoom = DataFakers.RoomFaker
                .RuleFor(room => room.Description, faker => faker.Lorem.Sentence(2))
                .Generate();
            var request = new CreateUserInRoomRequest(validUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(Result.Success<Room, ValidationResult>(existingRoom));

            _userReadOnlyRepositoryMock
                .GetByCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(Result.Success<Domain.Entities.User.User, ValidationResult>(existingRoom.Users.Last()));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }
    }
}