import { BrowserRouter, Routes, Route } from "react-router-dom";

import LayoutPublico from "../components/publico/LayoutPublico";
//import LayoutAdmin from "../components/admin/LayoutAdmin";

// Paginas publicas
import Inicio from "../paginas/publico/Inicio";
import ReservarTurno from "../paginas/publico/ReservarTurno";
import Login from "../paginas/publico/Login";

// Paginas admin

function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>

        // PUBLICO
        <Route element={<LayoutPublico />}>
          <Route path="/" element={<Inicio />} />

          <Route path="/reservar" element={<ReservarTurno />} />

          <Route path="/login" element={<Login />} />
        </Route>

        // ADMIN


      </Routes>
    </BrowserRouter>
  );
}

export default AppRouter;
