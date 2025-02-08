﻿using Microsoft.AspNetCore.Http;
using MindSpace.Domain.Services.Authentication;
using OfficeOpenXml;

namespace MindSpace.Application.Features.Authentication.Services
{
    public class ExcelReaderService : IExcelReaderService
    {
        public async Task<List<Dictionary<string, string>>> ReadExcelAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new BadHttpRequestException("No file uploaded.");
            }

            if (!file.FileName.EndsWith(".xlsx"))
            {
                throw new BadHttpRequestException("Invalid file type.");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                return ExtractData(stream);
            }
        }

        private List<Dictionary<string, string>> ExtractData(MemoryStream stream)
        {
            var results = new List<Dictionary<string, string>>();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension?.Rows ?? 0;
                int colCount = worksheet.Dimension?.Columns ?? 0;

                if (rowCount < 2) // At least header row and one data row
                {
                    return results;
                }

                // Read headers from first row
                var headers = new string[colCount];
                for (int col = 1; col <= colCount; col++)
                {
                    headers[col - 1] = worksheet.Cells[1, col].Text.Trim();
                }

                // Read data rows
                for (int row = 2; row <= rowCount; row++)
                {
                    var rowData = new Dictionary<string, string>();

                    for (int col = 1; col <= colCount; col++)
                    {
                        var header = headers[col - 1];
                        var value = worksheet.Cells[row, col].Text.Trim();

                        if (!string.IsNullOrEmpty(header))
                        {
                            rowData[header] = value;
                        }
                    }

                    if (rowData.Any())
                    {
                        results.Add(rowData);
                    }
                }
            }

            return results;
        }
    }
}