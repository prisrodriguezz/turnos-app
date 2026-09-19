import "../../estilos/publico/sobreNegocio.css";
import imgSobreNegocio from "../../assets/imagenes/sobreNegocio.jpg";

function SobreNegocio() {
  return (
    <section id="sobre-negocio" className="sobre-negocio">
      <div className="sobre-negocio-header">
        <span className="sobre-etiqueta">Conocenos</span>

        <h2 className="sobre-titulo">Un espacio pensado para tu bienestar</h2>
      </div>

      <div className="sobre-grid">
        <article className="sobre-card">
          <img src={imgSobreNegocio} alt="Aura Estética" className="sobre-imagen"/>

          <h3>Sobre Aura Estética</h3>

          <p>
            En Aura Estética creemos que el bienestar comienza dedicándote un
            momento para vos.
          </p>

          <p>
            Nuestro objetivo es brindar una experiencia cálida, profesional y
            personalizada, utilizando productos de calidad y tratamientos
            adaptados a cada cliente.
          </p>
        </article>

        <aside className="info-card">
          <h3>Información</h3>

          <ul className="info-lista">
            <li>
              <strong>📍 Dirección</strong>
              <span>Corrientes Capital</span>
            </li>

            <li>
              <strong>🕒 Horarios</strong>
              <span>Lunes a Viernes · 09:00 a 19:00</span>
            </li>

            <li>
              <strong>📞 Teléfono</strong>
              <span>(379) 4XX-XXXX</span>
            </li>

            <li>
              <strong>✉ Email</strong>
              <span>contacto@auraestetica.com</span>
            </li>

            <li>
              <strong>📷 Instagram</strong>
              <span>@auraestetica</span>
            </li>
          </ul>
        </aside>
      </div>
    </section>
  );
}

export default SobreNegocio;
