//import { useState } from 'react'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import AgentPage from "./pages/AgentPage";
//import PropertyPage from "./pages/PropertyPage";
import LoginPage from "./pages/LoginPage";
import DashboardPage from "./pages/DashboardPage";
import PhotographyCompaniesPage from "./pages/PhotographyCompaniesPage";


function App() {
  


  return (
    <main>
        <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/dashboard" element={<DashboardPage />} />
        <Route path="/agent" element={<AgentPage />} />
        <Route path="/photography-companies" element={<PhotographyCompaniesPage />} />
      </Routes>
    </BrowserRouter>
      

     
      
    </main>
  );


}

export default App;
