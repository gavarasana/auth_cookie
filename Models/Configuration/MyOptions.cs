namespace ravi.learn.web.cookieauth.Models.Configuration
{
    public class MyOptions
    {
        public MyOptions()
        {
            Option2 = "value_from_ctor";
        }

        public int Option1 { get; set; }
        public string Option2 { get; set; }
    }
}
