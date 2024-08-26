import React, { useEffect, useContext } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import './AdditionalContentPage.css';
import { UserContext } from '../context/UserContext'; // Importa el contexto del usuario
import config from '../config/config'; // Asegúrate de tener la configuración correcta

// Importa las imágenes necesarias
import image1 from '../media/bohr.png';
import image2 from '../media/fuegos-artificiales-san-juan-y-san-pedro-leon-2019.png';
import image3 from '../media/Heisenberg.png';
import image4 from '../media/sublimacion-1.jpg'
// Agrega los demás imports de imágenes aquí

const AdditionalContentPage = () => {
  const { experimentName } = useParams();
  const navigate = useNavigate();
  const { user } = useContext(UserContext); // Obtener el contexto del usuario

  
    const additionalContent = {
      'Color a la Llama': [
        {
          imgSrc: image1,
          description: 'Los principales problemas del modelo atómico de Rutherford eran la incompatibilidad con las leyes electromagnéticas, el movimiento de los electrones en torno al núcleo y las inexplicables líneas espectrales observadas en los átomos.',
        },
        {
          imgSrc: image2,
          description: 'Uso en la vida real: Identificación de elementos metálicos en muestras desconocidas. Análisis cualitativo de compuestos metálicos. Control de calidad en la industria, especialmente en la fabricación de productos químicos. Estudios arqueológicos y geológicos. En los fuegos artificiales.',
        },
        {
          imgSrc: image3,
          description: 'En el episodio piloto de la serie televisiva Breaking Bad, el profesor Walt White enseña a los alumnos cómo cambia el color de la llama de un mechero Bunsen cuando la rocía con distintas disoluciones de productos químicos.',
        },
      ],
      'Sublimación del Yodo Sólido': [
        {
          imgSrc: image4,
          description: 'La sublimación es el proceso mediante el cual una sustancia pasa directamente de sólido a gas sin pasar por el estado líquido. Esto ocurre porque las moléculas de yodo tienen fuerzas intermoleculares débiles, lo que permite que se vaporizen a bajas temperaturas.',
        },
        {
          imgSrc: 'image5',
          description: 'Uso en la vida real: La sublimación se utiliza en la purificación de sustancias, como en la industria farmacéutica para purificar compuestos volátiles. También es un proceso clave en la fabricación de algunos semiconductores y en la impresión en papel fotográfico.',
        },
        {
          imgSrc: 'image6',
          description: 'Dato curioso: Durante la Segunda Guerra Mundial, se utilizó el yodo sublimado para desinfectar el agua potable en los campos de batalla debido a sus propiedades bactericidas.',
        },
      ],
      'Experimento de Destilación': [
        {
          imgSrc: 'image7',
          description: 'La destilación es un proceso de separación que se basa en las diferencias de punto de ebullición de los componentes de una mezcla. Es fundamental en la obtención de productos puros en la industria química y alimentaria.',
        },
        {
          imgSrc: 'image8',
          description: 'Uso en la vida real: La destilación es ampliamente utilizada en la purificación de agua, la destilación de bebidas alcohólicas y en la refinación de petróleo para obtener productos como gasolina, queroseno y diésel.',
        },
        {
          imgSrc: 'image9',
          description: 'Dato curioso: El primer destilador fue creado por los alquimistas árabes en el siglo VIII, quienes destilaban alcohol para utilizarlo en la medicina.',
        },
      ],
      'Ley de Gases': [
        {
          imgSrc: 'image10',
          description: 'Las leyes de los gases describen el comportamiento de un gas ideal bajo condiciones de temperatura, presión y volumen. Estas leyes son fundamentales para entender procesos termodinámicos y se aplican en múltiples disciplinas científicas y tecnológicas.',
        },
        {
          imgSrc: 'image11',
          description: 'Uso en la vida real: Las leyes de los gases son cruciales en la ingeniería química, en el diseño de motores de combustión interna y en la producción de productos químicos a gran escala. También se aplican en la medicina, por ejemplo, en los tanques de oxígeno.',
        },
        {
          imgSrc: 'image12',
          description: 'Dato curioso: Robert Boyle, quien formuló la ley de Boyle en 1662, es considerado uno de los padres de la química moderna. Su trabajo marcó el inicio del estudio sistemático de las propiedades de los gases.',
        },
      ],
      'Experimento de Rutherford': [
        {
          imgSrc: 'image13',
          description: 'El experimento de Rutherford reveló que el átomo tiene un núcleo pequeño y denso rodeado por electrones en un espacio vacío. Este experimento fue crucial para desarrollar el modelo nuclear del átomo.',
        },
        {
          imgSrc: 'image14',
          description: 'Uso en la vida real: El modelo nuclear del átomo ha permitido el desarrollo de tecnologías como la energía nuclear, la medicina nuclear y la datación por carbono, que se basa en la desintegración de isótopos radiactivos.',
        },
        {
          imgSrc: 'image15',
          description: 'Dato curioso: Rutherford fue el primero en desintegrar un átomo artificialmente, lo que le valió el título de "Padre de la Física Nuclear".',
        },
      ],
      'Sodio Metálico y Agua': [
        {
          imgSrc: 'image16',
          description: 'La reacción del sodio metálico con el agua es altamente exotérmica y libera hidrógeno gaseoso, lo que puede causar explosiones si no se maneja adecuadamente. Esta reacción ilustra la reactividad de los metales alcalinos.',
        },
        {
          imgSrc: 'image17',
          description: 'Uso en la vida real: La reactividad del sodio y otros metales alcalinos se utiliza en baterías, donde estos metales reaccionan para producir energía eléctrica.',
        },
        {
          imgSrc: 'image18',
          description: 'Dato curioso: El sodio es tan reactivo que se almacena bajo aceite o queroseno para evitar que entre en contacto con la humedad del aire.',
        },
      ],
      'Camaleón Químico': [
        {
          imgSrc: 'image19',
          description: 'El camaleón químico es una demostración de una reacción redox, donde un reactivo cambia de color a medida que se oxida o reduce. Este experimento es comúnmente utilizado para ilustrar las reacciones de oxidación y reducción en la química.',
        },
        {
          imgSrc: 'image20',
          description: 'Uso en la vida real: Las reacciones redox son fundamentales en procesos industriales, como la galvanización, la producción de productos químicos, y en baterías donde las reacciones redox generan electricidad.',
        },
        {
          imgSrc: 'image21',
          description: 'Dato curioso: El permanganato de potasio, utilizado en el experimento de camaleón químico, se ha utilizado históricamente como desinfectante en heridas y como antiséptico.',
        },
      ],
      'Lluvia Dorada': [
        {
          imgSrc: 'image22',
          description: 'La "lluvia dorada" se refiere a la precipitación de cristales dorados de yoduro de plomo, que se forman al mezclar soluciones de yoduro de potasio y nitrato de plomo. Este experimento es un ejemplo clásico de una reacción de precipitación.',
        },
        {
          imgSrc: 'image23',
          description: 'Uso en la vida real: Las reacciones de precipitación se utilizan en la purificación de agua, donde se eliminan iones metálicos disueltos al formar precipitados que luego se pueden filtrar.',
        },
        {
          imgSrc: 'image24',
          description: 'Dato curioso: El yoduro de plomo era un pigmento utilizado en pinturas en la antigüedad, pero su uso ha disminuido debido a la toxicidad del plomo.',
        },
      ],
      'Identificación Ácido-base': [
        {
          imgSrc: 'image25',
          description: 'La identificación de ácidos y bases se realiza comúnmente con indicadores como el papel tornasol o la fenolftaleína. Estos indicadores cambian de color en presencia de un ácido o una base, lo que permite determinar el pH de la solución.',
        },
        {
          imgSrc: 'image26',
          description: 'Uso en la vida real: Los indicadores de pH son esenciales en la industria alimentaria, la fabricación de productos farmacéuticos y el tratamiento de aguas residuales, donde el control del pH es crucial.',
        },
        {
          imgSrc: 'image27',
          description: 'Dato curioso: El término "ácido" proviene del latín "acidus", que significa "agrio". Los antiguos romanos ya conocían las propiedades ácidas del vinagre y el jugo de limón.',
        },
      ],
      'Pasta de Dientes para Elefantes': [
        {
          imgSrc: 'image28',
          description: 'La "pasta de dientes para elefantes" es una demostración espectacular de la descomposición del peróxido de hidrógeno en agua y oxígeno, catalizada por yoduro de potasio. Esta reacción es altamente exotérmica y produce una gran cantidad de espuma.',
        },
        {
          imgSrc: 'image29',
          description: 'Uso en la vida real: El peróxido de hidrógeno se utiliza como agente blanqueador y desinfectante en aplicaciones médicas, industriales y domésticas. También se utiliza en la propulsión de cohetes debido a su capacidad para liberar oxígeno rápidamente.',
        },
        {
          imgSrc: 'image30',
          description: 'Dato curioso: Aunque el experimento de la "pasta de dientes para elefantes" es seguro bajo condiciones controladas, la liberación rápida de oxígeno y calor puede ser peligrosa a gran escala. Por eso, nunca se debe realizar sin las precauciones adecuadas.',
        },
      ],
      'Solución conductora': [
        {
          imgSrc: 'image31',
          description: 'Una solución conductora es una mezcla que permite el paso de corriente eléctrica debido a la presencia de iones libres. Las sales disueltas en agua son ejemplos clásicos de soluciones conductoras, donde los iones permiten que la electricidad fluya a través del líquido.',
        },
        {
          imgSrc: 'image32',
          description: 'Uso en la vida real: Las soluciones conductoras son fundamentales en baterías, donde la solución electrolítica permite el flujo de iones entre los electrodos. También se utilizan en la electroforesis para separar moléculas en laboratorios de biología molecular.',
        },
        {
          imgSrc: 'image33',
          description: 'Dato curioso: El agua destilada, aunque es un líquido, no conduce electricidad debido a la ausencia de iones. Sin embargo, al agregarle una pequeña cantidad de sal, se convierte en una solución conductora.',
        },
      ],
    };
   
  // Obtener el contenido basado en el nombre del experimento
  const content = additionalContent[experimentName] || [];

  useEffect(() => {
    const updateProgress = async () => {
      const quizIndex = Object.keys(additionalContent).indexOf(experimentName) + 1;
      const progressIndex = quizIndex * 5 - 3; // Calcula el índice para el 3er elemento del subgrupo

      try {
        await fetch(`${config.backendUrl}/updateStudent/${user.username}/prog/${progressIndex}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
          },
        });
      } catch (err) {
        console.error('Error al actualizar el progreso:', err);
      }
    };

    updateProgress();
  }, [experimentName, user.username]); // Dependencias

  return (
    <div className="additional-content-page-container col-12">
                    <nav className="navbar col-12">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
    </nav>
      <h1 className='display-3'>Contenido Adicional: {experimentName}</h1>
      <div className="content-grid col-12">
        {content.map((item, index) => (
          <div key={index} className="content-item col-3.5">
            <img className='img-fluid' src={item.imgSrc} alt={`Contenido ${index + 1}`} />
            <p className='col-12 text-align-center'>{item.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default AdditionalContentPage;