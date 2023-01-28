using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ExamTask.Helpers
{
    public  static class Helper
    {
        public static void FileDelete(string env,string path,string imagename)
        {
            string fullpath=Path.Combine(env,path,imagename);
            File.Delete(fullpath);
        }
    }
}
