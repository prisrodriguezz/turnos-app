import "../../estilos/publico/servicios.css";

function ServicioCard({ icono, nombre, descripcion, duracion }) {
  return (
    <article className="servicio-card">

      <div className="servicio-icono">{icono}</div>

      <h3>{nombre}</h3>

      <p>{descripcion}</p>

      <span>{duracion}</span>
    </article>
  );
}

export default ServicioCard;
