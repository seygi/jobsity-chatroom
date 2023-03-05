using FluentValidation;
using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase;
using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.Validators;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.ChatroomMessages.GetMessagesRoom.Validators
{
    public class GetMessagesRoomValidatorTest
    {
        private readonly GetMessagesRoomValidator _validator;

        public GetMessagesRoomValidatorTest()
        {
            _validator = new GetMessagesRoomValidator();
        }

        [Fact]
        public void Should_Have_Error_When_RommId_Is_Null_Or_Empty()
        {
            var model = GetMessagesRoomInput.Create(Guid.Empty, null);
            var result = _validator.Validate(model);

            // assert
            result.IsValid.Should().BeFalse();
            result.Errors.Where(x => x.PropertyName == "ChatRoomId").Count().Should().Be(1);
        }

        [Fact]
        public void Should_Return_True_When_Has_All_Correct_Data()
        {
            var model = GetMessagesRoomInput.Create(Guid.NewGuid(), null);
            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
            result.Errors.Count().Should().Be(0);
        }
    }
}
