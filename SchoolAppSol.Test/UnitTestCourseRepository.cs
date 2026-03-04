using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Persitence.Exceptions;

namespace SchoolAppSol.Test
{
    public class UnitTestCourseRepository
    {
        [Fact]
        public async Task Add_Course_When_CourseEntity_IsNull_Async()
        {
            //Arrange
            Course? course = null;

            //Act
            var exception = await Assert.ThrowsAsync<PersistenceException>(() => Task.FromResult(course));

        }
        [Fact]
        public async Task Add_Course_When_Title_IsNull_Async()
        {
            //Arrange
            Course? course = new Course() { Title = null };

            //Act
            var exception = await Assert.ThrowsAsync<PersistenceException>(() => Task.FromResult(course));

        }
        [Fact]
        public async Task Add_Course_When_Title_Lenght_Greater_100_Async()
        {
            //Arrange
            Course? course = new Course() { Title = "asdfasdfasdfsadfasdfasdfasdfasdfasdfasdfdfasdasddfasdfsadfasddfaasdsdasdaasdfasdasfdsfdaafsdsadfsda" };

            //Act
            var exception = await Assert.ThrowsAsync<PersistenceException>(() => Task.FromResult(course));

        }
        [Fact]
        public async Task Add_Course_When_Title_Lenght_Greater_100_Async()
        {
            //Arrange
            Course? course = new Course() { Title = "asdfasdfasdfsadfasdfasdfasdfasdfasdfasdfdfasdasddfasdfsadfasddfaasdsdasdaasdfasdasfdsfdaafsdsadfsda" };

            //Act
            var exception = await Assert.ThrowsAsync<PersistenceException>(() => Task.FromResult(course));

        }
    }
}