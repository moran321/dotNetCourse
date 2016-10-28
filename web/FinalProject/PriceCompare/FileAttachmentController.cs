    [RoutePrefix("files")]
    public class FilesController : ApiController
    {

        [GET("somepdf/{id}")]
        public HttpResponseMessage GetSomePdf(string id)
        {
            var path = MyApp.PathToSomePdf(id);
            if (path!= null)
                return FileAsAttachment(path, "somepdf.pdf");
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        public static HttpResponseMessage FileAsAttachment(string path, string filename)
        {
            if (File.Exists(path))
            {

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(path, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = filename;
                return result;
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }