using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Contract.ICommon;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using AutoMapper;


namespace Logic.UnitTests.CourseFeature
{
    [TestFixture]
    public class CourseServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IBaseRepository<Course>> _mockCourseRepository;
        private Mock<IMapper> _mockMapper;
        private ICourseService _courseService;

        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCourseRepository = new Mock<IBaseRepository<Course>>();
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Course>())
                           .Returns(_mockCourseRepository.Object);

            _courseService = new CourseService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnMappedCourse_WhenCourseExists()
        {
            // Arrange
            var courseId = new Guid("12345678-1234-1234-1234-123456789012");
            var expectedCourse = new Course { ID = courseId, Name = "Test Course" };
            var expectedCourseDto = new GetCourseDto { ID = courseId, Name = "Test Course" };


            _mockCourseRepository.Setup(repo => repo.GetByIdAsync(courseId))
                                 .ReturnsAsync(expectedCourse);

            _mockMapper.Setup(m => m.Map<GetCourseDto>(It.IsAny<Course>()))
                       .Returns(expectedCourseDto);

            // Act
            var result = await _courseService.GetByIdAsync(courseId);

            // Assert   
            Assert.That(result, Is.EqualTo(expectedCourseDto));
        }
    }
}
