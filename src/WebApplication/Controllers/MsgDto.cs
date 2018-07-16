namespace WebApplication.Controllers
{
    public class MsgDto
    {
        public MsgDto() { }
        public MsgDto(string msg)
        {
            Msg = msg;
        }

        public string Msg { get; set; }
    }
}