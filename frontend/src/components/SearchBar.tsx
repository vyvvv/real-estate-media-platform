function SearchBar({
  showCreateButton = true,
  placeholder = "Search from listing case...",
   createButtonLabel = "+ Create Property",
    inputClassName = "py-2"
  
}: {
  showCreateButton?: boolean;
  placeholder?: string;
  createButtonLabel?: string;
  inputClassName?: string;

}) {
  return (
    <div className="w-full">
      <div className="items-center flex justify-between gap-4 h-10">
          {showCreateButton && <div className="flex-1" />}

        <div className={showCreateButton ? "flex-1 max-w-md": "w-full"}>
          <label htmlFor="search" className="sr-only ">
            Search
          </label>

          <input
            id="search"
            type="text"
            placeholder={placeholder}
            className={`w-full rounded-full border border-gray-200 bg-gray-300 px-4 ${inputClassName} text-sm text-gray-900 shadow-sm outline-none placeholder:text-gray-400 focus:border-blue-500 focus:ring-2 focus:ring-blue-200`}
          />
        </div>
         {showCreateButton && (
        <div className="flex-1 flex justify-center">
             <button className="rounded-md px-4 py-2 text-sm font-medium text-white bg-sky-600 hover:bg-blue-700 whitespace-nowrap">
                {createButtonLabel}
             </button>
         
        </div>
         )}
      </div>
    </div>
  );
}

export default SearchBar;
