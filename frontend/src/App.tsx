//import { useState } from 'react'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import AgentPage from "./pages/AgentPage";
//import PropertyPage from "./pages/PropertyPage";
import LoginPage from "./pages/LoginPage";
import AdminDashboardPage from "./pages/AdminDashboardPage";
import PhotographyCompaniesPage from "./pages/PhotographyCompaniesPage";
import PropertyPage from "./pages/PropertyPage";
import AgentDashBoardPage from "./pages/AgentDashBoardPage";
import SignUpPage from "./pages/SignUpPage";
import PropertyDetailPage from "./pages/PropertyDetailPage";


function App() {
  


  return (
    <main>
        <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/dashboard" element={<AdminDashboardPage />} />
        <Route path="/agent" element={<AgentPage />} />
        <Route path="/photography-companies" element={<PhotographyCompaniesPage />} />
        <Route path="/property/:id" element={<PropertyPage />} /> 
        <Route path="/agentdashboard" element={<AgentDashBoardPage />} />
        <Route path="/signup" element={<SignUpPage />} />
        <Route path="/propertydetail" element={<PropertyDetailPage />} />

      </Routes>
    </BrowserRouter>
      

     
      
    </main>
  );


}

export default App;
