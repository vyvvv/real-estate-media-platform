import { FileBlob, SpreadsheetFile } from "@oai/artifact-tool";

const path = "C:/Users/Administrator/Desktop/房地产项目信息/20260526095535_updated_recam_tasks.xlsx";
const input = await FileBlob.load(path);
const workbook = await SpreadsheetFile.importXlsx(input);
const overview = await workbook.inspect({
  kind: "workbook,sheet,table",
  maxChars: 12000,
  tableMaxRows: 20,
  tableMaxCols: 12,
  tableMaxCellChars: 300,
});
console.log(overview.ndjson);
