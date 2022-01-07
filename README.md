# ReportGenerator
A simple scalable API to generate report file (mainly PDF) based on user input (html string, urls)

## Usage
Use postman or other POST capable tools to send POST request to (for example in localhost, as configured in launchSettings.json) address : http://localhost:8080

## Method & Parameters

- url: http://localhost:8080/api/pdfconverter/convertfromhtml

| Get Parameter Name | Mandatory | Type | Get Value Example |
|----------------|-----------|------|--------|
| None | None | None | None |

| Post Parameter Name | Mandatory | Type | Parameter Value Example |
|----------------|-----------|------|--------|
| htmlInput | Yes | String | "\<h1\>Hello World\</h1\>" |
| fileNameWithoutExtension | Yes | String | "test_file" |
| orientation  | No | String | only "portrait" or "landscape" |
| pageNumbering  | No | Boolean | True |

- output: downloadable PDF file (with the name of "test_file.pdf" as defined in the example)
