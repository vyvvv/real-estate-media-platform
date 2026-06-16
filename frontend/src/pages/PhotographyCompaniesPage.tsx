import LoginNavBar from "../components/LoginNavBar";
import SearchBar from "../components/SearchBar";

function PhotographyCompaniesPage() {
  return (
    <div>
      <LoginNavBar />

      <main className="min-h-screen bg-gray-50 flex flex-col items-center pt-14 px-4">
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold tracking-tight text-gray-900">
           Hi, Welcome!
          </h1>
        </div>
        

       <div className="mt-12 w-112">
        <SearchBar showCreateButton={false} placeholder="Search from your photography companies..." />
         </div>
        

         


      </main>
    </div>
  );
}
     

export default PhotographyCompaniesPage;
