import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.css';
import { AuthProvider } from './auth/AuthProvider';
import HomePage from './app/pages/HomePage';
import MainLayout from './app/components/MainLayout';
import LoginPage from './app/pages/LoginPage';
import RegisterPage from './app/pages/RegisterPage';
import CarsListPage from './app/pages/car/CarListingPage';
import CarCreatePage from './app/pages/car/CarCreatePage';
import CarUpdatePage from './app/pages/car/CarUpdatePage';
import CarViewPage from './app/pages/car/CarViewPage';
import RequireEmployee from './auth/RequireEmployee';
import ServiceCarsListPage from './app/pages/service-car/ServiceCarListingPage';
import ServiceCarCreatePage from './app/pages/service-car/ServiceCarCreatePage';
import ServiceCarViewPage from './app/pages/service-car/ServiceCarViewPage';
import ServiceCarUpdatePage from './app/pages/service-car/ServiceCarUpdate';
import SaleListingPage from './app/pages/sale/SaleListingPage';
import SaleCreatePage from './app/pages/sale/SaleCreatePage';
import SaleViewPage from './app/pages/sale/SaleViewPage';
import SaleUpdatePage from './app/pages/sale/SaleUdpatePage';
import RequireCustomer from './auth/RequireCustomer';
import ServiceRecordListingPage from './app/pages/service-record/ServiceRecordListingPage';
import ServiceRecordCreatePage from './app/pages/service-record/ServiceRecordCreatePage';
import ServiceRecordViewPage from './app/pages/service-record/ServiceRecordViewPage';
import ServiceRecordUpdatePage from './app/pages/service-record/ServiceRecordUpdatePage';
import RequireUser from './auth/RequireUser';


function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<MainLayout />}>
          <Route index element={<HomePage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/cars" element={<CarsListPage />} />
          <Route path="/cars/create" element={<RequireEmployee><CarCreatePage /></RequireEmployee>} />
          <Route path="/cars/update/:id" element={<RequireEmployee><CarUpdatePage /></RequireEmployee>} />
          <Route path="/cars/:id" element={<CarViewPage />} />
          <Route path="/service-cars/my" element={<RequireCustomer><ServiceCarsListPage /></RequireCustomer>} />
          <Route path="/service-cars/my/create" element={<RequireCustomer><ServiceCarCreatePage /></RequireCustomer>} />
          <Route path="/service-cars/my/:id" element={<RequireCustomer><ServiceCarViewPage /></RequireCustomer>} />
          <Route path="/service-cars/my/update/:id" element={<RequireCustomer><ServiceCarUpdatePage /></RequireCustomer>} />
          <Route path="/sales" element={<RequireEmployee><SaleListingPage /></RequireEmployee>} />
          <Route path="/sales/create" element={<RequireEmployee><SaleCreatePage /></RequireEmployee>} />
          <Route path="/sales/:id" element={<RequireEmployee><SaleViewPage /></RequireEmployee>} />
          <Route path="/sales/update/:id" element={<RequireEmployee><SaleUpdatePage /></RequireEmployee>} />
          <Route path="/service-records" element={<RequireUser><ServiceRecordListingPage /></RequireUser>} />
          <Route path="/service-records/create" element={<RequireEmployee><ServiceRecordCreatePage /></RequireEmployee>} />
          <Route path="/service-records/:id" element={<RequireUser><ServiceRecordViewPage /></RequireUser>} />
          <Route path="/service-records/update/:id" element={<RequireEmployee><ServiceRecordUpdatePage /></RequireEmployee>} />
          
          </Route>
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
