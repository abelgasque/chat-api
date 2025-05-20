import os

FRAME_EMBEDDABLE = True
BABEL_DEFAULT_LOCALE = "pt_BR"
ENABLE_PROXY_FIX = True
SESSION_COOKIE_SAMESITE = None
PUBLIC_ROLE_LIKE_GAMMA = True
ENABLE_REFERER_HEADER = False
WTF_CSRF_ENABLED = False
SQLALCHEMY_DATABASE_URI = f"{os.getenv('DATABASE_URI')}"

FEATURE_FLAGS = {
  "EMBEDDED_SUPERSET": True,
  "ENABLE_TEMPLATE_PROCESSING": True,
}

LANGUAGES = {
  "en": {"flag": "us", "name": "Inglês"},
  "es": {"flag": "es", "name": "Espanhol"},
  "pt_BR": {"flag": "br", "name": "Português"},
}

from jwt_security import CustomSecurityManager
CUSTOM_SECURITY_MANAGER = CustomSecurityManager