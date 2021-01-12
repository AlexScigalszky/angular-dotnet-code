using Domain.Constants;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Data.Seeds
{
    public class MailTemplateSeed
    {
        public static IEnumerable<MailTemplate> GetData()
        {
            return new MailTemplate[]
            {
                MailTemplate.Create(MailConstants.ID_RECOVERY_PASSWORD, "Recuperar contraseña", "SEAPOL - Link para reestablecer contraseña", $"<br><p>Estimado Usuario {MailConstants.KEY_USER_MAIL}</p><br><p>Haga click en el siguiente link para reestablecer su contraseña.</p><br><p><a href=\"{MailConstants.KEY_LINK_TO_REDIRECT}\" target=\"_blank\" rel=\"noopener\">{MailConstants.KEY_LINK_TO_REDIRECT}</a></p><br><p>En caso que el link no lo redireccione, puede copiarlo y pegarlo en su navegador de preferencia</p><br><p>Recuerde, no responda a este mail. Es un envío automático.</p><br><p>Gracias,</p>"),
            };
        }
    }
}
