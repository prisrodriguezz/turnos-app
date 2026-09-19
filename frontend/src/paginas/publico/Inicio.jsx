import Hero from "../../components/publico/Hero";
import Servicios from "../../components/publico/Servicios";
import SobreNegocio from "../../components/publico/SobreNegocio";

function Inicio() {
  const servicios = [
    {
      id: 1,
      icono: "💅",
      nombre: "Manicura",
      descripcion: "Cuidado y embellecimiento de uñas.",
      duracion: "60 minutos",
    },
    {
      id: 2,
      icono: "✨",
      nombre: "Limpieza Facial",
      descripcion: "Tratamiento para revitalizar tu piel.",
      duracion: "60 minutos",
    },
    {
      id: 3,
      icono: "💆",
      nombre: "Masajes",
      descripcion: "Relajación y bienestar corporal.",
      duracion: "50 minutos",
    },
  ];

  return (
    <section>
      <>
        <Hero />
        <Servicios servicios={servicios}/>
        <SobreNegocio/>
      </>
    </section>
  );
}

export default Inicio;
