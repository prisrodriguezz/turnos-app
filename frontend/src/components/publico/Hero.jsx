import { Link } from "react-router-dom";
import heroImage from "../../assets/imagenes/aura-estetica.jpg";

import "../../estilos/publico/hero.css";

function Hero() {
  return (
    <section id="hero" className="hero">
      <div className="hero-contenido">
        <span className="hero-etiqueta">✨ Bienvenidos a </span>

        <h1 className="hero-titulo">Aura Estética</h1>

        <h2 className="hero-subtitulo">
          Belleza, bienestar y tiempo para vos.
        </h2>

        <p className="hero-descripcion">
          Disfrutá de una experiencia pensada para tu bienestar. Elegí el
          servicio que necesitás y reservá tu turno online en pocos pasos.
        </p>

        <div className="hero-botones">
          <Link to="/reservar" className="btn btn--primary">
            Reservar turno
          </Link>

          <a href="#servicios" className="btn btn--secondary">
            Ver servicios
          </a>
        </div>
      </div>

      <div className="hero-imagen">

        <img src={heroImage} alt="Aura Estética"/>

      </div>
    </section>
  );
}

export default Hero;
