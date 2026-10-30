namespace DemoTest.Data
{
public class FormData
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class TestData
    {
        public FormData InvalidFormData { get; set; }
        public FormData ValidFormData { get; set; }
    }
}