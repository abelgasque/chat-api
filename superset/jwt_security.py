import os
import jwt
import logging
from flask import redirect, flash, request
from flask_appbuilder.security.views import AuthDBView
from flask_appbuilder.security.views import expose
from flask_login import login_user
from superset.security import SupersetSecurityManager

class CustomAuthDBView(AuthDBView):

    @expose('/login/', methods=['GET', 'POST'])
    def login(self):
        token = request.args.get('token')
        sm = self.appbuilder.sm
        session = sm.get_session
        if not token:
            return super(CustomAuthDBView,self).login()
        try:
            decoded_token = jwt.decode(token, os.getenv('SUPERSET_SECRET_KEY'), algorithms=["HS256"])
            id = decoded_token["sub"]
            user = session.query(sm.user_model).filter_by(id=id).first()
            login_user(user, remember=False, force=True)
            return redirect(request.args.get('to') or '/superset/welcome')
        except jwt.ExpiredSignatureError as e:
            logging.error("Token JWT expirado.")
            flash('Seu token de login expirou. Faça login novamente.', 'warning')
            return super(CustomAuthDBView, self).login()
        except Exception as e:
            logging.error(f"Erro no login: {e}")
            flash('Erro ao realizar auto login', 'warning')
            return super(CustomAuthDBView, self).login()

class CustomSecurityManager(SupersetSecurityManager):
    authdbview = CustomAuthDBView
    def __init__(self, appbuilder):
        super(CustomSecurityManager, self).__init__(appbuilder)