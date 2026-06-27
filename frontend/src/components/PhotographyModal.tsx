import { useState } from "react";

const PhotographyModal = ({ onFilesChange }: { onFilesChange: (files: File[]) => void }) => {
  const [files, setFiles] = useState<File[]>([]);


  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
  if (!e.target.files) return;
  const selected = [...e.target.files].slice(0,9);
  setFiles(selected);        // 本地显示文件名
  onFilesChange(selected);   // 传给父组件
};


  return (
    <div className="border-2 border-dashed border-gray-300 rounded-lg p-10 flex flex-col items-center gap-4">
      <p className="text-sm text-gray-500">Drop your images here to upload</p>
      <input id="photoInput" type="file" accept="image/*" multiple onChange={handleFileChange} className="hidden" />
      <label htmlFor="photoInput" className="rounded-full bg-sky-600 text-white px-4 py-2 text-sm cursor-pointer hover:bg-sky-700">
        Choose Files
      </label>
     {files.length > 0 && (
  <div className="grid grid-cols-3 gap-2 mt-2 w-full">
    {files.map((file, index) => (
      <div key={file.name} className="relative">
        <img
          src={URL.createObjectURL(file)}
          className="w-full h-20 object-cover rounded"
        />
        <button
          type="button"
          onClick={() => {
            const updated = files.filter((_, i) => i !== index);
            setFiles(updated);
            onFilesChange(updated);
          }}
          className="absolute top-1 right-1 bg-red-500 text-white rounded-full w-5 h-5 flex items-center justify-center text-xs hover:bg-red-600"
        >
          ×
        </button>
      </div>
    ))}
  </div>
)}
    </div>
  );
};
export default PhotographyModal;
