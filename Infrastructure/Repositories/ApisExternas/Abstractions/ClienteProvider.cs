namespace Infrastructure.Repositories.ApisExternas.Abstractions
{
    using Application.Contracts.ApiExterna.Abstractions;
    using Application.Models.ConsumoApi.Models;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.Net.Http.Headers;
    using System.Text;

    public class ClienteProvider : IClienteProvider
    {
        public async Task<ResponseModel<T>> GetAsync<T>(string urlBase, string path,
            string? tokenType = null, string? accessToken = null)
        {
            try
            {
                var client = new HttpClient();
                //Agregamos el encabezado
                //*********************************************************************************************
                if (!string.IsNullOrEmpty(tokenType) && !string.IsNullOrEmpty(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                }
                //*********************************************************************************************

                client.BaseAddress = new Uri(urlBase);
                var url = $"{path}";
                var ResponseModel = await client.GetAsync(url);
                if (typeof(T) == typeof(Stream))
                {
                    Stream stream = await ResponseModel.Content.ReadAsStreamAsync();
                    return GenerateSuccessResponse<T>((T)(object)stream);
                }
                else
                {
                    var answer = await ResponseModel.Content.ReadAsStringAsync();
                    return !ResponseModel.IsSuccessStatusCode
                    ? GenerateErrorResponse<T>(answer, ResponseModel)
                        : GenerateSuccessResponse<T>(answer);
                }
            }
            catch (Exception ex)
            {
                return GenerateExceptionResponse<T>(ex.Message);
            }
        }
        public async Task<ResponseModel<T>> GetAsyncWithQueryParams<T>(string urlBase, string path, Dictionary<string, string> queryParams,
            string? tokenType = null, string? accessToken = null)
        {
            try
            {
                string queryParamsCollection = string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));
                var client = new HttpClient();
                //Agregamos el encabezado
                //*********************************************************************************************
                if (!string.IsNullOrEmpty(tokenType) && !string.IsNullOrEmpty(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                }
                //*********************************************************************************************

                client.BaseAddress = new Uri(urlBase);
                var url = $"{path}?{queryParamsCollection}";
                var ResponseModel = await client.GetAsync(url);
                var answer = await ResponseModel.Content.ReadAsStringAsync();

                return !ResponseModel.IsSuccessStatusCode
                    ? GenerateErrorResponse<T>(answer, ResponseModel, $"QueryParams: {queryParamsCollection}")
                    : GenerateSuccessResponse<T>(answer);
            }
            catch (Exception ex)
            {
                return GenerateExceptionResponse<T>(ex.Message);
            }
        }
        public async Task<ResponseModel<T>> PostAsyncFormData<T>(string urlBase, string path, Dictionary<string, string> formData,
            string? tokenType = null, string? accessToken = null)
        {
            try
            {
                var client = new HttpClient();
                //Agregamos el encabezado
                //*********************************************************************************************
                if (!string.IsNullOrEmpty(tokenType) && !string.IsNullOrEmpty(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                }
                //*********************************************************************************************
                client.BaseAddress = new Uri(urlBase);
                var url = $"{path}";

                var content = new FormUrlEncodedContent(formData);
                var formDataString = string.Join(" | ", formData.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));

                var ResponseModel = await client.PostAsync(url, content);

                var answer = await ResponseModel.Content.ReadAsStringAsync();

                return !ResponseModel.IsSuccessStatusCode
                    ? GenerateErrorResponse<T>(answer, ResponseModel, $"FormData: {formDataString}")
                    : GenerateSuccessResponse<T>(answer);
            }
            catch (Exception ex)
            {
                return GenerateExceptionResponse<T>(ex.Message);
            }
        }

        public async Task<ResponseModel<T>> PostAsyncFormData<T>(string urlBase, string path, Dictionary<string, string> formData,
        IFormFile file, string? tokenType = null, string? accessToken = null)
        {
            try
            {
                using var client = new HttpClient();

                if (!string.IsNullOrEmpty(tokenType) && !string.IsNullOrEmpty(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                }

                client.BaseAddress = new Uri(urlBase);

                using var content = new MultipartFormDataContent();

                using var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.FileName);

                foreach (var item in formData)
                {
                    content.Add(new StringContent(item.Value), item.Key);
                }

                var response = await client.PostAsync(path, content);

                var answer = await response.Content.ReadAsStringAsync();

                return !response.IsSuccessStatusCode
                    ? GenerateErrorResponse<T>(
                        answer,
                        response,
                        formData.Any()
                            ? $"FormData: {string.Join(" | ", formData.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"))} | file= {file.FileName}"
                            : $"FormData: file={file.FileName}"
                        )
                    : GenerateSuccessResponse<T>(answer);
            }
            catch (Exception ex)
            {
                return GenerateExceptionResponse<T>(ex.Message);
            }
        }
        public async Task<ResponseModel<T>> PostAsyncJson<T>(string urlBase, string path, Object model,
            string? tokenType = null, string? accessToken = null)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                //Agregamos el encabezado
                //*********************************************************************************************
                if (!string.IsNullOrEmpty(tokenType) && !string.IsNullOrEmpty(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                }
                //*********************************************************************************************

                client.BaseAddress = new Uri(urlBase);
                var url = $"{path}";
                var ResponseModel = await client.PostAsync(url, content);
                var answer = await ResponseModel.Content.ReadAsStringAsync();

                return !ResponseModel.IsSuccessStatusCode
                    ? GenerateErrorResponse<T>(answer, ResponseModel, request)
                    : GenerateSuccessResponse<T>(answer);
            }
            catch (Exception ex)
            {
                return GenerateExceptionResponse<T>(ex.Message);
            }
        }
        public async Task<ResponseModel<T>> UploadFileAsync<T>(string urlBase, string path, string filePath,
            string? tokenType = null, string? accessToken = null)
        {
            try
            {
                var client = new HttpClient();

                //Agregamos el encabezado
                //*********************************************************************************************
                if (!string.IsNullOrEmpty(tokenType) && !string.IsNullOrEmpty(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                }
                //*********************************************************************************************
                client.BaseAddress = new Uri(urlBase);
                var url = $"{path}";
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        var streamContent = new StreamContent(fileStream);
                        streamContent.Headers.ContentType = new MediaTypeHeaderValue(GetMimeType(filePath));
                        var getFileName = Path.GetFileName(filePath);

                        content.Add(streamContent, "file", getFileName);
                        // Log the MIME type of the file
                        Console.WriteLine($"File MIME type: {streamContent.Headers.ContentType}");

                        var ResponseModel = await client.PostAsync(url, content);
                        var answer = await ResponseModel.Content.ReadAsStringAsync();

                        return !ResponseModel.IsSuccessStatusCode
                            ? GenerateErrorResponse<T>(answer, ResponseModel)
                            : GenerateSuccessResponse<T>(answer);
                    }
                }
            }
            catch (Exception ex)
            {
                return GenerateExceptionResponse<T>(ex.Message);
            }

        }
        public async Task<ResponseModel<T>> PutAsync<T>(string urlBase, string path, Object model, int id,
            string? tokenType = null, string? accessToken = null)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                //Agregamos el encabezado
                //*********************************************************************************************
                if (!string.IsNullOrEmpty(tokenType) && !string.IsNullOrEmpty(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                }
                //*********************************************************************************************
                var url = $"{path}/{id}";
                var ResponseModel = await client.PutAsync(url, content);
                var answer = await ResponseModel.Content.ReadAsStringAsync();

                return !ResponseModel.IsSuccessStatusCode
                    ? GenerateErrorResponse<T>(answer, ResponseModel, request)
                    : GenerateSuccessResponse<T>(answer);
            }
            catch (Exception ex)
            {
                return GenerateExceptionResponse<T>(ex.Message);
            }
        }
        public async Task<ResponseModel<T>> DeleteAsync<T>(string urlBase, string path,
            string? tokenType = null, string? accessToken = null)
        {
            try
            {
                var client = new HttpClient();
                //Agregamos el encabezado
                //*********************************************************************************************
                if (!string.IsNullOrEmpty(tokenType) && !string.IsNullOrEmpty(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                }
                //*********************************************************************************************

                client.BaseAddress = new Uri(urlBase);
                var url = $"{path}";
                var ResponseModel = await client.DeleteAsync(url);
                var answer = await ResponseModel.Content.ReadAsStringAsync();

                return !ResponseModel.IsSuccessStatusCode
                    ? GenerateErrorResponse<T>(answer, ResponseModel)
                    : GenerateSuccessResponse<T>(answer);
            }
            catch (Exception ex)
            {
                return GenerateExceptionResponse<T>(ex.Message);
            }
        }
        private static string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            switch (extension)
            {
                case ".txt": return "text/plain";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/msword";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".csv": return "text/csv";
                case ".xml": return "text/xml";
                case ".json": return "application/json";
                case ".zip": return "application/zip";
                case ".rar": return "application/x-rar-compressed";
                case ".7z": return "application/x-7z-compressed";
                case ".mp3": return "audio/mpeg";
                case ".wav": return "audio/wav";
                case ".mp4": return "video/mp4";
                case ".mkv": return "video/x-matroska";
                case ".mov": return "video/quicktime";
                case ".avi": return "video/x-msvideo";
                case ".ppt": return "application/vnd.ms-powerpoint";
                case ".pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".odt": return "application/vnd.oasis.opendocument.text";
                case ".ods": return "application/vnd.oasis.opendocument.spreadsheet";
                case ".odp": return "application/vnd.oasis.opendocument.presentation";

                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".jfif": return "image/jpeg";
                case ".pjp": return "image/jpeg";
                case ".jpe": return "image/jpeg";
                case ".gif": return "image/gif";
                case ".bmp": return "image/bmp";
                case ".ico": return "image/vnd.microsoft.icon";
                case ".tiff": return "image/tiff";
                case ".svg": return "image/svg+xml";
                case ".webp": return "image/webp";
                case ".svgz": return "image/svg+xml";
                case ".xbm": return "image/x-xbitmap";
                case ".dib": return "image/bmp";
                case ".avif": return "image/avif";
                case ".apng": return "image/apng";
                case ".pjpeg": return "image/pjpeg";
                case ".tif": return "image/tiff";
                case ".jp2": return "image/jp2";
                case ".jpx": return "image/jpx";
                case ".jpm": return "image/jpm";
                case ".mj2": return "image/mj2";
                case ".heif": return "image/heif";
                case ".heic": return "image/heif";
                case ".avci": return "image/avci";
                case ".avcs": return "image/avcs";

                case ".css": return "text/css";
                case ".html": return "text/html";
                case ".htm": return "text/html";
                case ".php": return "application/php";
                case ".js": return "text/javascript";
                case ".go": return "text/x-go";
                case ".py": return "text/x-python";
                case ".java": return "text/x-java-source";
                case ".c": return "text/x-c";
                case ".cpp": return "text/x-c++src";
                case ".cs": return "text/x-csharp";
                case ".swift": return "text/x-swift";
                case ".sh": return "application/x-shellscript";
                case ".sql": return "application/sql";
                case ".rtf": return "application/rtf";
                case ".tar": return "application/x-tar";
                case ".bin": return "application/octet-stream";
                default: return "application/octet-stream"; // Tipo genérico de datos binarios
            }
        }
        private ResponseModel<T> GenerateErrorResponse<T>(string answer, HttpResponseMessage response, string? requestBody = null)
        {
            var errorDetails = new ErrorClientProviderDetails
            {
                IsSuccess = false,
                Message = "Ocurrió un error durante la conexión con la API",
                RequestUrl = response.RequestMessage?.RequestUri?.ToString(),
                RequestMethod = response.RequestMessage?.Method.ToString(),
                RequestBody = requestBody,
                ApiResponse = answer,
                HttpStatusCode = (int)response.StatusCode,
                TimeStamp = DateTime.UtcNow
            };
            var serializedError = JsonConvert.SerializeObject(errorDetails);

            return new ResponseModel<T>
            {
                IsSuccess = false,
                Message = serializedError
            };
        }
        private ResponseModel<T> GenerateSuccessResponse<T>(object data)
        {
            T obj;
            if (typeof(T) == typeof(Stream))
            {
                obj = (T)data;
            }
            else
            {
                string answer = data as string;
                try
                {
                    obj = JsonConvert.DeserializeObject<T>(answer);
                }
                catch (JsonReaderException)
                {
                    obj = (T)Convert.ChangeType(answer, typeof(T));
                }
            }

            return new ResponseModel<T>
            {
                IsSuccess = true,
                Result = obj,
                Message = "Petición exitosa"
            };
        }
        private ResponseModel<T> GenerateExceptionResponse<T>(string message)
        {
            return new ResponseModel<T>
            {
                IsSuccess = false,
                Message = "Se ha generado una excepción en el procesamiento de la peteción: " + message,
            };
        }

    }
}
