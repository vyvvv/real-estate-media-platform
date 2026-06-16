import { useState } from "react";

const tabs = ["All", "Created", "Pending", "Delivered"];

function TabFilter() {
  const [activeTab, setActiveTab] = useState("All");

  return (
    <nav className="p-4">
      <div className="flex flex-col gap-2 ">
        {tabs.map((tab) => (
          <button
            key={tab}
            onClick={() => setActiveTab(tab)}
            className={`px-4 py-3 rounded-md text-sm text-center transition 
              ${activeTab === tab 
                ? "bg-gray-600 font-medium"   
                : "text-gray-600 hover:bg-gray-500"  
              }`}
          >
            {tab}
          </button>
        ))}
      </div>
    </nav>
  );
}

export default TabFilter;