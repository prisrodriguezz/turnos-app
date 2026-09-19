import "../../estilos/publico/FooterPublico.css";

function FooterPublico() {
  return (
    <footer className="footer-publico">
      <div className="footer-logo">
        <h3>✿ Aura Estética</h3>

        <p>Belleza, bienestar y tiempo para vos.</p>
      </div>

      <div className="footer-redes">
        <a href="https://www.instagram.com/">Instagram</a>

        <a href="https://www.facebook.com/">Facebook</a>

        <a href="https://www.whatsapp.com/">WhatsApp</a>
      </div>

      <div className="footer-copy">
        <p>© 2026 Aura Estética. Todos los derechos reservados.</p>
      </div>
    </footer>
  );
}

export default FooterPublico;
