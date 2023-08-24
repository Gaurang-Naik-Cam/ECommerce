using CanteenEnrolment.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection.Metadata;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CanteenEnrolment.UnitTests
{
    public class UnitTest1
    {
        [TestMethod]
        public void Student_ShouldCreate_With_Valid_properties()
        {

            var student = new Student
            {
                ID = 3,
                FirstMidName = "John",
                 LastName  = "Doe",
                EnrollmentDate = new DateTime(2023, 8, 6)
            };

            Assert.AreEqual(3, student.ID);
            Assert.AreEqual("John", student.FirstMidName);
            Assert.AreEqual("Doe", student.LastName);
            Assert.AreEqual(new DateTime(2023, 8, 6), student.EnrollmentDate);
         
        }
    }
}