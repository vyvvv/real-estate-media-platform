import houseImg from "../assets/house.jpg";
<img src={houseImg} className="w-full h-full object-cover" />;

const PropertyDetailPage = () => {
  return (
    <>
      <div className="flex h-[400px]">
        {/* 左侧图片 */}
        <div className="w-3/5">
          <img
            src="https://images.unsplash.com/photo-1570129477492-45c003edd2be?w=800"
            className="w-full h-full object-cover"
          />
        </div>

        {/* 右侧介绍 */}
        <div className="w-2/5 bg-[#3D1A10] flex flex-col items-center justify-center gap-4 text-white px-12">
          {/* 标签 */}
          <div className="border border-white text-xs px-3 py-1 rounded-sm">
            Apartment for Rent
          </div>

          {/* 地址 */}
          <div className="text-3xl font-bold">93 Beach Road</div>
          <div className="text-sm text-gray-300">North Bondi, NSW, 2026</div>

          {/* 分隔线 */}
          <div className="w-8 border-b-2 border-white mt-2" />

          {/* 房屋信息 */}
          <div className="flex gap-6 text-sm mt-2">
            <div className="flex flex-col items-center gap-1">
              <span>🛏</span>
              <span>2 Beds</span>
            </div>
            <div className="flex flex-col items-center gap-1">
              <span>🚿</span>
              <span>2 Baths</span>
            </div>
            <div className="flex flex-col items-center gap-1">
              <span>🚗</span>
              <span>2 Garages</span>
            </div>
            <div className="flex flex-col items-center gap-1">
              <span>📐</span>
              <span>112 m²</span>
            </div>
          </div>
        </div>
      </div>
{/* 
      <div className="min-h-screen bg-gray-50 flex flex-col items-center pt-14 px-4">
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold tracking-tight text-gray-900">
            Property Description
          </h1>

          <p className="mt-2 text-sm text-gray-600 pt-5">
            Please Add Property Description Here{" "}
            <div className="font-medium underline pt-5">Click to Add</div>
          </p>
        </div>
      </div> */}

{/* Photography */}
<div className="py-10 px-16">
  <h2 className="text-center text-xl font-semibold mb-6">Photography</h2>
  <div className="grid grid-cols-3 gap-2">
    {Array.from({ length: 9 }).map((_, i) => (
      <img
        key={i}
        src="https://images.unsplash.com/photo-1570129477492-45c003edd2be?w=400"
        className="w-full h-48 object-cover"
      />
    ))}
  </div>
</div>

{/* Floor Plan */}
<div className="py-10 px-16">
  <h2 className="text-center text-xl font-semibold mb-6">Floor Plan</h2>
  <div className="flex justify-center">
    <img
      src="https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=800"
      className="w-full max-w-3xl object-cover"
    />
  </div>
</div>

{/* Videography */}
<div className="py-10 px-16">
  <h2 className="text-center text-xl font-semibold mb-6">Videography</h2>
  <div className="flex justify-center">
    <video
      controls
      className="w-full max-w-3xl"
      src="https://www.w3schools.com/html/mov_bbb.mp4"  
    />
  </div>
</div>

{/* Location */}
<div className="py-10 px-16">
  <h2 className="text-center text-xl font-semibold mb-6">Location</h2>
  <div className="flex justify-center">
    {/* TODO: 替换成 Google Maps */}
    <div className="w-full max-w-3xl h-80 bg-gray-200 flex items-center justify-center rounded-md">
      <span className="text-gray-400 text-sm">Map placeholder</span>
    </div>
  </div>
</div>
    </>
  );
};

export default PropertyDetailPage;
