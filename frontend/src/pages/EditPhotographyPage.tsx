
import LoginNavBar from "../components/LoginNavBar";
import Modal from "../components/Modal";
import PhotographyModal from "../components/PhotographyModal";
import { useNavigate } from "react-router-dom";

import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import PropertyBreadcrumb from "../components/PropertyBreadcrumb";
import { getListingById } from "../apis/listingcases.api";
import type { Property } from "../types/Property";

const EditPhotographyPage = () => {
  const [images, setImages] = useState<string[]>([]);
  const [isOpen, setIsOpen] = useState(false);
  const navigate = useNavigate();

  const { id } = useParams();
const [property, setProperty] =
  useState<Property | null>(null);

  useEffect(() => {
  if (!id) {
    return;
  }

  async function loadProperty() {
    const data = await getListingById(Number(id));

    setProperty({
      ...data,
      createdAt: new Date(data.createdAt),
    });
  }

  loadProperty();
}, [id]);


  const [pendingFiles, setPendingFiles] = useState<File[]>([]);

  const handleUpload = () => {
    const urls = pendingFiles.map((file) => URL.createObjectURL(file));
    setImages((prev) => [...prev, ...urls]);
    setIsOpen(false);
  };

  return (
    <>
      <LoginNavBar />
      <div className="min-h-screen bg-gray-100 p-20">
        {/* 标题置顶居中 */}
        <h1 className="text-2xl font-bold text-center mb-10">Hi, Welcome!</h1>

        {/* 面包屑在左边 */}
        <div className="flex justify-between items-center mb-12">
          <PropertyBreadcrumb
  property={property}
  currentPage="Photography"
/>
          <button
            onClick={() => navigate(-1)}
            className="text-sm text-gray-500 hover:text-gray-700"
          >
            Back
          </button>
        </div>

        <div className="mb-6 flex justify-center">
          <button
            onClick={() => setIsOpen(true)}
            className="px-4 py-2 bg-sky-600 text-white rounded-full text-sm hover:bg-sky-700"
          >
            Upload Photos
          </button>
        </div>

        <div className="grid grid-cols-4 gap-4">
          {images.map((url, index) => (
            <div key={index} className="relative">
              <img src={url} className="w-full h-40 object-cover rounded" />
              <button
                type="button"
                onClick={() => setImages(images.filter((_, i) => i !== index))}
                className="absolute top-1 right-1 bg-red-500 text-white rounded-full w-5 h-5 flex items-center justify-center text-xs hover:bg-red-600"
              >
                ×
              </button>
            </div>
          ))}
        </div>

        <Modal
          title="Upload Photography"
          subtitle=""
          saveLabel="Upload"
          isOpen={isOpen}
          onClose={() => setIsOpen(false)}
          onSave={handleUpload} // 点击 Upload 按钮时执行
        >
          <PhotographyModal onFilesChange={setPendingFiles} />
        </Modal>
      </div>
    </>
  );
};
export default EditPhotographyPage;
