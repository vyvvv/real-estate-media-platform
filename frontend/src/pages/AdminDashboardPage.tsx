import LoginNavBar from "../components/LoginNavBar";
import SearchBar from "../components/SearchBar";

function AdminDashboardPage() {
  return (
    <div>
      <LoginNavBar />

      <main className="min-h-screen bg-gray-50 flex flex-col items-center pt-14 px-4">
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold tracking-tight text-gray-900">
           Hi, Welcome!
          </h1>
        </div>
        

       <div className="mt-12 w-full">
        <SearchBar showCreateButton={true} placeholder="Search from your listing case"    />
         </div>
        

         


      </main>
    </div>
  );
}
     

export default AdminDashboardPage;
