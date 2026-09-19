import Header from "../publico/HeaderAdmin";
import Footer from "../publico/FooterAdmin";
import { Outlet } from "react-router-dom";

function LayoutAdmin() {
    return (
        <div>

            <Header/>

            <main>
                <Outlet/>
            </main>

            <Footer/>

        </div>
    );
}

export default LayoutAdmin;