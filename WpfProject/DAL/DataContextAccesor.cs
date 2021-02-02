namespace WpfProject.DAL
{
    public class DataContextAccesor
    {
        private static DataContext context;
        private DataContextAccesor()
        {
        }

        public static DataContext GetDataContext()
        {
            if (context == null)
                context = new DataContext();

            return context;
        }

    }
}
