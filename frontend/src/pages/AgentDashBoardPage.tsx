import SearchBar from "../components/SearchBar";
import TabFilter from "../components/TabFilter";
import PropertyCard from "../components/PropertyCard";

function AgentDashBoardPage() {
  return (
    <div className="w-full shadow-sm ">
      <div className="mx-auto flex-col h-[100px] items-center px-20 border-b-2 border-gray-300">
        <div className="text-xs text-gray-500 mt-8">Hi, I'm Jane</div>

        <div className="w-full flex justify-between mt-5 items-center">
    <div className="text-base font-bold whitespace-nowrap shrink-0">
      My Property
    </div>
    <div className="w-64">
      <SearchBar
        showCreateButton={false}
        placeholder="Search My Property"
        inputClassName="py-1"
      />
    </div>
  </div>


      </div>

      
      <main className="flex gap-60 p-4  ">
        <div className="w-48 shrink-0 ml-30 ">
          <TabFilter />
        </div>
        <div className="flex-1 max-w-7xl w-full">
          <PropertyCard/>
        </div>
      </main>
    </div>
  );
}

export default AgentDashBoardPage;
