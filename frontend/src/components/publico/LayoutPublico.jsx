import Header from "./HeaderPublico";
import Footer from "./FooterPublico";
import { Outlet } from "react-router-dom";

function LayoutPublico() {
    return (
        <div className="layout-publico">

            <Header/>

            <main>
                <Outlet/>
            </main>

            <Footer/>

        </div>
    );
}

export default LayoutPublico;