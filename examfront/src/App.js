import logo from './logo.svg';
import './App.css';
import { RegistrationPage } from './Registration/RegistrationPage';
import { AuthorizationPage } from './Authorization/AuthorizationPage';
import { MainPage } from './Main/MainPage';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
        <Routes>
          <Route
            path="/authorize"
            element={<AuthorizationPage/>}
          />
          <Route
            path="/register"
            element={<RegistrationPage/>}
          />
          <Route
            path="*"
            element={<MainPage/>}
          />
        </Routes>
      </BrowserRouter>
  );
}

export default App;
