import { useState } from "react";

const PropertyCard = () => {
  // 暂时用假数据
  const id = "Order # 000-000-000-001";
  const createdAt = "13 November 2024";
  const address = "93 Beach Road, North Bondi, NSW, 2026";
  const price = "$800";
  const status = "Pending";

  const [selected,setSelected] = useState(false);

  return (
    
    <div onClick={()=> setSelected(!selected)}  
    className={`bg-white rounded-lg border border-gray-200 p-4 mb-4 hover:ring-2 hover:ring-sky-500 cursor-pointer ${selected ? "ring-2 ring-sky-500" : ""}`}>
      
      {/* 第一行：订单号 + 状态标签 */}
      <div className="flex justify-between items-center mb-1">
        <div className="text-sm font-semibold text-gray-700">{id}</div>
        <div className="bg-orange-400 text-white text-xs px-3 py-1 rounded-md">
          {status}
        </div>
      </div>

      {/* 创建时间 */}
      <div className="text-xs text-gray-400 mb-3">Ordered on {createdAt}</div>

      {/* 第二行：地址 + 总价 */}
      <div className="flex justify-between items-start">
        <div>
          {/* 地址 */}
          <div className="text-base font-semibold text-gray-800 mb-2">{address}</div>

          {/* 服务标签 */}
          <div className="flex gap-2">
            <button className="border border-gray-300 text-xs px-2 py-1 rounded-md text-gray-600">
              📷 Photography
            </button>
            <button className="border border-gray-300 text-xs px-2 py-1 rounded-md text-gray-600">
              Floor Plan
            </button>
            <button className="border border-gray-300 text-xs px-2 py-1 rounded-md text-gray-600">
              🎥 Videography
            </button>
          </div>
        </div>

        {/* 总价 */}
        <div className="text-right">
          <div className="text-xs text-gray-400">Total</div>
          <div className="text-xl font-bold text-gray-800">{price}</div>
        </div>
      </div>

      {/* 查看详情 */}
      <div className="flex justify-end mt-3">
        <a href="/propertydetail" className="text-xs text-gray-400 hover:text-gray-600">
          View order details →
        </a>
      </div>

    </div>
  );
};

export default PropertyCard;