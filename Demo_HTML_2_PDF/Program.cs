
using Demo_HTML_2_PDF;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.Text;

var wkHtmlToPdfPath = System.AppContext.BaseDirectory + "\\wkhtmltox\\libwkhtmltox";
CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(wkHtmlToPdfPath);

IConverter _converter = new BasicConverter(new PdfTools()); //Usar SynchronizedConverter para multhilo y web, registrarlo como Singleton
//APARECE ESTE ERROR, LEER: Qt: Could not initialize OLE (error 80010106)
//https://stackoverflow.com/questions/47021112/qt-could-not-initialize-ole-error-80010106-libwkhtmltox-dll-on-c-sharp

Console.WriteLine("Ingresa tu nombre: ");
string userName = Console.ReadLine().ToString();
var html = $"<h1>Hola PDF, soy {userName}</h1>";
var doc = new HtmlToPdfDocument()
{
    GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    Out = "Demo.pdf"
                },
    Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html
                    }
                }
};
_converter.Convert(doc);
Console.WriteLine("PDF Creado en teoria");
Console.ReadKey();


    //private string ConvertUserListToHtmlTable(List<User> users)
    //{
    //    var header1 = "<th>Username</th>";
    //    var header2 = "<th>Name</th>";
    //    var header3 = "<th>Surname</th>";
    //    var header4 = "<th>Email Address</th>"; var headers = $"<tr>{header1}{header2}{header3}{header4}</tr>"; var rows = new StringBuilder(); foreach (var user in users)
    //    {
    //        var column1 = $"<td>{user.UserName}</td>";
    //        var column2 = $"<td>{user.Name}</td>";
    //        var column3 = $"<td>{user.Surname}</td>";
    //        var column4 = $"<td>{user.EmailAddress}</td>"; var row = $"<tr>{column1}{column2}{column3}{column4}</tr>"; rows.Append(row);
    //    }
    //    return $"<table>{headers}{rows.ToString()}</table>";
    //}

public class FileDto
{
    public string FileName { get; set; }
    public byte[] FileBytes { get; set; }
    public FileDto(string fileName, byte[] fileBytes)
    {
        FileName = fileName;
        FileBytes = fileBytes;
    }
}