// using System.Globalization;
// using Android;
// using Android.Content.PM;
// using AndroidX.Core.App;
// using AndroidX.Core.Content;
// using iText.IO.Image;
// using iText.Kernel.Events;
// using iText.Kernel.Geom;
// using iText.Kernel.Pdf;
// using iText.Layout;
// using iText.Layout.Element;
// using iText.Layout.Properties;
// using Image = iText.Layout.Element.Image;

// namespace TimeTracker.Manager;

// public static class ExportManager
// {
//     public static async Task RequestStoragePermissionsAsync()
//     {
//         if (ContextCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
//         {
//             ActivityCompat.RequestPermissions(Platform.CurrentActivity, new string[] { Manifest.Permission.WriteExternalStorage }, 1);
//         }
//     }
//     public static async Task CreateAndSavePdfAsync(List<WorkItemModel> workItemList, string expDate, string filePath,string kfzSign, string users)
//     {
//         try
//         {
//             DateTime expDateTime;
//             if (!DateTime.TryParseExact(expDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expDateTime))
//             {
//                 throw new FormatException("Das Datum hat nicht das erwartete Format.");
//             }

//             using (var memoryStream = new MemoryStream())
//             {
//                 using (var writer = new PdfWriter(memoryStream))
//                 {
//                     using (var pdf = new PdfDocument(writer))
//                     {
//                         var pageSize = PageSize.A4.Rotate();
//                         var doc = new Document(pdf,pageSize);

//                         pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new PageNumberEventHandler(doc, ">>Personalanwesenheitsbogen<< \n Tagesbogen"));

//                         if(users == "Nancy_Annette")
//                         {
//                             doc.Add(new Paragraph("Name: Nancy Fergens & Annette Reyer"));
//                         }
//                         else if(users == "Nancy")
//                         {
//                             doc.Add(new Paragraph("Name : Nancy Fergens"));
//                         }
//                         else
//                         {
//                             doc.Add(new Paragraph("Name : Annette Reyer"));
//                         }



                        


//                         Table AddTableHeader(float[] colWidth)
//                         {
                            
//                             var table = new Table(UnitValue.CreatePercentArray(colWidth));
//                             table.AddHeaderCell("Uhrzeit von").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                             table.AddHeaderCell("Uhrzeit bis").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                             table.AddHeaderCell("Pause").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                             table.AddHeaderCell("Dauer").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                             table.AddHeaderCell("Reinigungsart").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                             table.AddHeaderCell("Objekt").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                             table.AddHeaderCell("Hinweis").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                             return table;
//                         }
//                         float[] columnWidth = { 80, 80, 110, 110, 130, 150, 150 };
//                         var table = AddTableHeader(columnWidth);


//                         doc.Add(new Paragraph($"Datum: {expDate}"));
//                         doc.Add(new Paragraph($"Fahrzeugkennzeichen : {kfzSign}"));


//                         var filteredList = workItemList.Where(x => x.Date.ToString("dd.MM.yyyy") == expDate).OrderBy(y => y.From).ToList();
//                         var totalHours = 0;
//                         var totalMinutes = 0;

//                         int itemCount = 0;

//                         foreach (var item in filteredList)
//                         {
//                             if (item.Date.Date == expDateTime.Date)
//                             {
//                                 if (item.Type == ArtTypEnum.Fahrzeit.ToString() ||
//                                     item.Type == ArtTypEnum.Pause.ToString() ||
//                                     item.Type == ArtTypEnum.Arbeitszeit.ToString() ||
//                                     item.Type == ArtTypEnum.Wischbezüge.ToString() ||
//                                     item.Type == ArtTypEnum.Fahrzeugpflege.ToString())
//                                 {
//                                     //Uhrzeit VON
//                                     var fromHour = item.From.Hours.ToString();
//                                     var fromMin = item.From.Minutes.ToString("00");

//                                     //Uhrzeit Bis
//                                     var toHour = item.To.Hours.ToString();
//                                     var toMin = item.To.Minutes.ToString("00");

//                                     //Ergebnis Stunden-Minuten
//                                     var resultHour = item.Result.Hours.ToString();
//                                     var resultMin = item.Result.Minutes < 10 ? item.Result.Minutes.ToString() : item.Result.Minutes.ToString("00");

//                                     if(item.Type != ArtTypEnum.Pause.ToString())
//                                     {
//                                         totalHours += item.Result.Hours;
//                                         totalMinutes += item.Result.Minutes;
//                                     }


//                                     string formattedFrom = fromHour + ":" + fromMin;
//                                     string formattedTo = toHour + ":" + toMin;
//                                     string formattedResult = "";

//                                     if (resultHour != "0")
//                                     {
//                                         formattedResult = resultHour + " h " + resultMin + " min";
//                                     }
//                                     else
//                                     {
//                                         formattedResult = resultMin + " min";
//                                     }


//                                     //Zuweisungen
//                                     //Zuweisung Uhrzeit VON
//                                     table.AddCell(formattedFrom).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                     //Zuweisung Uhrzeit BIS
//                                     table.AddCell(formattedTo).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

//                                     if (item.Type == ArtTypEnum.Pause.ToString())
//                                     {
//                                         //Pausenzeit wurde eingetragen
//                                         table.AddCell(formattedResult).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                         //Es gibt keine Arbeitszeit
//                                         table.AddCell("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                     }
//                                     else
//                                     {
//                                         //Es gibt keine Pausenzeit
//                                         table.AddCell("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                         //Arbeitszeit wurde eingetragen
//                                         table.AddCell(formattedResult).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                     }
//                                     //Reinigungstyp wird eingetragen
//                                     table.AddCell(item.CleanType ?? "");

//                                     //Abfrage ob Object null ist
//                                     if (!string.IsNullOrEmpty(item.Object))
//                                     {
//                                         if (item.Type == ArtTypEnum.Fahrzeit.ToString())
//                                         {
//                                             table.AddCell("Fahrzeit").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                         }
//                                         else if (item.Type == ArtTypEnum.Arbeitszeit.ToString())
//                                         {
//                                             table.AddCell(item.Object).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                         }
//                                         else if (item.Type == ArtTypEnum.Wischbezüge.ToString())
//                                         {
//                                             table.AddCell("Wischbezüge " + item.Object).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                         }
//                                         else if (item.Type == ArtTypEnum.Pause.ToString())
//                                         {
//                                             table.AddCell("Pause").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                         }
//                                     }
//                                     else
//                                     {
//                                         if (item.Type == ArtTypEnum.Fahrzeit.ToString())
//                                             table.AddCell("Fahrzeit").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                         else if (item.Type == ArtTypEnum.Pause.ToString())
//                                             table.AddCell("Pause").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                         else if (item.Type == ArtTypEnum.Fahrzeugpflege.ToString())
//                                             table.AddCell("Fahrzeugpflege").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                         else
//                                             table.AddCell("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
//                                     }
//                                     if (item.Notice == "")
//                                         table.AddCell(" ");
//                                     else
//                                         table.AddCell($"{item.Notice}").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

//                                     itemCount++;
                                    
//                                     if(itemCount >= 12)
//                                     {
                                       
//                                         doc.Add(table);

//                                         doc.Add(new AreaBreak()); // Seitenumbruch hinzufügen
                                        
//                                         if (users == "Nancy_Annette")
//                                         {
//                                             doc.Add(new Paragraph("Name: Nancy Fergens & Annette Reyer"));
//                                         }
//                                         else if (users == "Nancy")
//                                         {
//                                             doc.Add(new Paragraph("Name : Nancy Fergens"));
//                                         }
//                                         else
//                                         {
//                                             doc.Add(new Paragraph("Name : Annette Reyer"));
//                                         }

//                                         doc.Add(new Paragraph($"Datum: {expDate}"));
//                                         doc.Add(new Paragraph($"Fahrzeugkennzeichen : {kfzSign}"));

//                                         float[] columnWidth2 = { 80, 80, 110, 110, 130, 150, 150 };
//                                         table = AddTableHeader(columnWidth2);           
//                                         itemCount = 0;
//                                     }

                                    
//                                 }
//                             }
//                         }

//                         if(itemCount > 0)
//                             doc.Add(table);

//                         var hourResult = totalMinutes / 60;
//                         hourResult += totalHours;
//                         var remainMinutes = totalMinutes % 60;


//                         if(hourResult > 1)
//                         {
//                             if (remainMinutes == 1)
//                             {
//                                 doc.Add(new Paragraph($"Gesamtstunden : {hourResult} Stunden {remainMinutes} Minute"));
//                             }
//                             else
//                             {
//                                 doc.Add(new Paragraph($"Gesamtstunden : {hourResult} Stunden {remainMinutes} Minuten"));
//                             }
//                         }
//                         else if(hourResult == 1)
//                         {
//                             if(remainMinutes == 1)
//                             {
//                                 doc.Add(new Paragraph($"Gesamtstunden : {hourResult} Stunde {remainMinutes} Minute"));
//                             }
//                             else
//                             {
//                                 doc.Add(new Paragraph($"Gesamtstunden : {hourResult} Stunde {remainMinutes} Minuten"));
//                             }
//                         }
//                         else if(hourResult < 1)
//                         {
//                             if (remainMinutes == 1)
//                             {
//                                 doc.Add(new Paragraph($"Gesamtstunden : {remainMinutes} Minute"));
//                             }
//                             else
//                             {
//                                 doc.Add(new Paragraph($"Gesamtstunden : {remainMinutes} Minuten"));
//                             }
//                         }


//                         if (users == "Nancy_Annette")
//                         {
//                             Table signTable = new Table(UnitValue.CreatePercentArray(3));
//                             signTable.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
//                             iText.Layout.Element.Cell newCell = new iText.Layout.Element.Cell().Add(new Paragraph("\n Unterschrift: "));
//                             newCell.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
//                             signTable.AddCell(newCell);

//                             byte[] signNFBytes = await OnLoadSignatureImageNancy();
//                             var imageData = ImageDataFactory.Create(signNFBytes);
//                             var nfImage = new Image(imageData);

//                             OnSetWidthHeightPerPercent(nfImage);

//                             iText.Layout.Element.Cell imgCell = new iText.Layout.Element.Cell().Add(nfImage);
//                             imgCell.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
//                             signTable.AddCell(imgCell);


//                             byte[] signARBytes = await OnLoadSignatureImageAnnette();
//                             var imageData2 = ImageDataFactory.Create(signARBytes);
//                             var arImage = new Image(imageData2);

//                             OnSetWidthHeightPerPercent(arImage);

//                             iText.Layout.Element.Cell imgCell2 = new iText.Layout.Element.Cell().Add(arImage);
//                             imgCell2.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
//                             signTable.AddCell(imgCell2);

//                             doc.Add(signTable);

//                         }
//                         else if (users == "Nancy")
//                         {
//                             Table signTable = new Table(UnitValue.CreatePercentArray(2));
//                             signTable.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
//                             iText.Layout.Element.Cell newCell = new iText.Layout.Element.Cell().Add(new Paragraph("\n Unterschrift: "));
//                             newCell.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
//                             signTable.AddCell(newCell);


//                             byte[] signImageBytes = await OnLoadSignatureImageNancy();
//                             var imageData = ImageDataFactory.Create(signImageBytes);
//                             var image = new Image(imageData);

//                             OnSetWidthHeightPerPercent(image);

//                             iText.Layout.Element.Cell imgCell = new iText.Layout.Element.Cell().Add(image);
//                             imgCell.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
//                             signTable.AddCell(imgCell);

//                             doc.Add(signTable);
//                         }
//                         else if (users == "Annette")
//                         {
//                             Table signTable = new Table(UnitValue.CreatePercentArray(2));
//                             signTable.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
//                             iText.Layout.Element.Cell newCell = new iText.Layout.Element.Cell().Add(new Paragraph("\n Unterschrift: "));
//                             newCell.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
//                             signTable.AddCell(newCell);

//                             byte[] signImageBytes = await OnLoadSignatureImageAnnette();
//                             var imageData = ImageDataFactory.Create(signImageBytes);
//                             var image = new Image(imageData);

//                             OnSetWidthHeightPerPercent(image);

//                             iText.Layout.Element.Cell imgCell = new iText.Layout.Element.Cell().Add(image);
//                             imgCell.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
//                             signTable.AddCell(imgCell);

//                             doc.Add(signTable);
//                         }


//                         doc.Close();

//                         File.WriteAllBytes(filePath, memoryStream.ToArray());

//                         await Shell.Current.DisplayAlert("Info", "Dokument wurde erstellt", "OK");
//                     }
//                 }
//             }
//         }
//         catch (Exception ex)
//         {
//             await Shell.Current.DisplayAlert("Fehler", $"Fehler beim Erstellen des Dokuments: {ex.Message}", "OK");
//         }
//     }
//     private static async Task<byte[]> OnLoadSignatureImageNancy()
//     {
//         var assembly = System.Reflection.Assembly.GetExecutingAssembly();

//         using(var stream = assembly.GetManifestResourceStream("TimeTracker.Resources.Images.sign_fneu.png"))
//         using (var memorystream = new MemoryStream())
//         {
//             if (stream == null)
//                 throw new FileNotFoundException($"Resource TimeTracker.Resources.Images.sign_fneu.png not found");

//             await stream.CopyToAsync(memorystream);
//             return memorystream.ToArray();
//         }
//     }
//     private static async Task<byte[]> OnLoadSignatureImageAnnette()
//     {
//         var assembly = System.Reflection.Assembly.GetExecutingAssembly();

//         using (var stream = assembly.GetManifestResourceStream("TimeTracker.Resources.Images.sign_rneu.png"))
//         using (var memorystream = new MemoryStream())
//         {
//             if (stream == null)
//                 throw new FileNotFoundException($"Resource TimeTracker.Resources.Images.sign_rneu.png not found");

//             await stream.CopyToAsync(memorystream);
//             return memorystream.ToArray();
//         }
//     }
//     private static void OnSetWidthHeightPerPercent(Image img)
//     {
//         var newImage = img;

//         float originalWidth = newImage.GetImageScaledWidth();
//         float originalHeight = newImage.GetImageScaledHeight();

//         float scalePercent = 75;

//         float newWidth = originalWidth * (scalePercent / 100);
//         float newHeight = originalHeight * (scalePercent / 100);

//         newImage.ScaleToFit(newWidth, newHeight);
//     }
//     private class PageNumberEventHandler : IEventHandler
//     {
//         private readonly Document _document;
//         private readonly string _header;

//         public PageNumberEventHandler(Document document, string header)
//         {
//             _document = document;
//             _header = header;
//         }
//         public void HandleEvent(Event @event)
//         {
//             PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
//             PdfDocument pdfDoc = docEvent.GetDocument();
//             PdfPage page = docEvent.GetPage();
//             int pageNumber = pdfDoc.GetPageNumber(page);
//             Rectangle pageSize = page.GetPageSize();
//             float x = (pageSize.GetLeft() + pageSize.GetRight()) / 2;
//             float yHeader = pageSize.GetTop() - 35;
//             float yFooter = pageSize.GetBottom() + 20;
//             Canvas canvas = new Canvas(page, pageSize);


//             canvas.ShowTextAligned(_header, x, yHeader, iText.Layout.Properties.TextAlignment.CENTER);

//             canvas.ShowTextAligned($"Seite {pageNumber}", x, yFooter, iText.Layout.Properties.TextAlignment.CENTER);
//         }
//     }


// }